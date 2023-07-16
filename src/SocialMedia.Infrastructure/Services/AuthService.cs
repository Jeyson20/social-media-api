using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.Commands.RefreshToken;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Helpers;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Utils;
using SocialMedia.Domain.Entities.Users;
using SocialMedia.Domain.Enums;
using SocialMedia.Infrastructure.Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocialMedia.Infrastructure.Services
{
    public sealed class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _context;
		private readonly JwtConfig _jwtConfig;
		private const int TokenExpiration = 1;
		private const int RefreshTokenLength = 40;

		public AuthService(ApplicationDbContext context, IOptions<JwtConfig> jwtConfig)
		{
			_context = context;
			_jwtConfig = jwtConfig.Value;
		}

		public async Task<AuthDto> AuthenticateAsync(AuthenticateCommand command, CancellationToken cancellationToken = default)
		{
			var userExist = await _context.Users
				.Include(x => x.Token)
				.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken)
				?? throw new ApiException("User or password is incorrect.");

			if (userExist.Status != Status.Active)
				throw new ApiException("Your user account isn't activated. Please communicate with an administrator.");

			bool isPasswordValid = PasswordHelper.VerifyPassword(command.Password, userExist.Password!);
			if (!isPasswordValid)
				throw new ApiException("User or password is incorrect.");

			string token = GenerateJwtToken(userExist);
			string refreshToken = GenerateRefreshToken();

			var hashedRefreshToken = PasswordHelper.HashPassword(refreshToken);

			if (userExist.Token is null)
				userExist.SetUserToken(hashedRefreshToken);
			else
				userExist.Token.UpdateUsertoken(hashedRefreshToken);

			await _context.SaveChangesAsync(cancellationToken);

			return new AuthDto(token, refreshToken);
		}

		public async Task<TokenDto> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
		{
			var hashToken = PasswordHelper.HashPassword(command.RefreshToken);

			var tokenExist = await _context.UserTokens
				.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Token == hashToken, cancellationToken)
				?? throw new ApiException("The provided token is invalid.");

			DateTime currentDate = DateTime.Now;

			if (tokenExist.Expiration < currentDate)
				throw new ApiException("The provided token has expired.");

			string newToken = GenerateJwtToken(tokenExist.User!);

			return new TokenDto(newToken);
		}

		private string GenerateJwtToken(User user)
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
				expires: DateTime.UtcNow.AddHours(TokenExpiration),
				signingCredentials: new SigningCredentials(
				new SymmetricSecurityKey(secretKey),
				SecurityAlgorithms.HmacSha256Signature)

				);

			return tokenHandler.WriteToken(jwtOptions);

		}
		private static string GenerateRefreshToken()
		{
			using var randomNumberGenerator = RandomNumberGenerator.Create();
			var tokenBytes = new byte[RefreshTokenLength];
			randomNumberGenerator.GetBytes(tokenBytes);
			return Convert.ToBase64String(tokenBytes);
		}
		private ClaimsPrincipal ValidateToken(string token)
		{

			var tokenHandler = new JwtSecurityTokenHandler();

			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = _jwtConfig.Issuer,
				ValidateAudience = true,
				ValidAudience = _jwtConfig.Audience,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret)),
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			try
			{
				ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
				return claimsPrincipal;
			}
			catch (SecurityTokenValidationException)
			{
				throw new ApiException("Token provided has expired or is invalid.");
			}
		}
	}
}
