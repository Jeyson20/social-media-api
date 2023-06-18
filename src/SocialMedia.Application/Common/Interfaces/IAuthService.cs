using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<TokenDto> AuthenticateAsync(AuthenticateCommand command, CancellationToken cancellationToken = default);
	}
}
