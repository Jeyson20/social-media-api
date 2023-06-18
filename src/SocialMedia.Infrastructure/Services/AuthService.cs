using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Helpers;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Utils;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;
using SocialMedia.Infrastructure.Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Infrastructure.Services
{
	public sealed class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _context;
		private readonly JwtConfig _jwtConfig;
		private const int TokenExpiration = 1;
		private const int RefreshTokenExpiration = 24;


		public AuthService(ApplicationDbContext context, IOptions<JwtConfig> jwtConfig)
		{
			_context = context;
			_jwtConfig = jwtConfig.Value;
		}

		public async Task<TokenDto> AuthenticateAsync(AuthenticateCommand command, CancellationToken cancellationToken = default)
		{
			var userExist = await _context.Users
				.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken)
				?? throw new ApiException("User or password is incorrect.");

			if (userExist.Status != Status.Active)
				throw new ApiException("Your user account isn't activated. Please communicate with an administrator.");

			bool isPasswordValid = PasswordHelper.VerifyPassword(command.Password, userExist.Password!);
			if (!isPasswordValid)
				throw new ApiException("User or password is incorrect.");

			string token = GenerateJwtToken(userExist, TokenExpiration);
			string refreshToken = GenerateJwtToken(userExist, RefreshTokenExpiration);

			return new TokenDto(token, refreshToken);
		}


		private string GenerateJwtToken(User user, int expiration)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secretKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret ?? "");

			var claims = new List<Claim>()
				{
					new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Name, user.Username!),
					new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				};

			var jwtOptions = new JwtSecurityToken(
				issuer: _jwtConfig.Issuer,
				audience: _jwtConfig.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddHours(expiration),
				signingCredentials: new SigningCredentials(
				new SymmetricSecurityKey(secretKey),
				SecurityAlgorithms.HmacSha256Signature)

				);

			return tokenHandler.WriteToken(jwtOptions);

		}
	}
}
