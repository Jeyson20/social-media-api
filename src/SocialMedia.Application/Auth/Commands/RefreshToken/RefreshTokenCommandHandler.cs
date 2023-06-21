using MediatR;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Common.Interfaces;

namespace SocialMedia.Application.Auth.Commands.RefreshToken
{
	internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
	{
		private readonly IAuthService _authService;

		public RefreshTokenCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			return await _authService.RefreshTokenAsync(request, cancellationToken);
		}
	}
}
