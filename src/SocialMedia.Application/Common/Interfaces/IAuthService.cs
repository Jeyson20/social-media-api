using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.Commands.RefreshToken;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<AuthDto> AuthenticateAsync(AuthenticateCommand command, CancellationToken cancellationToken = default);
		Task<TokenDto> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default);
	}
}
