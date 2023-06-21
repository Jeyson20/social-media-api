using MediatR;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Common.Interfaces;

namespace SocialMedia.Application.Auth.Commands.Authenticate
{
	internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthDto>
	{
		private readonly IAuthService _authService;

		public AuthenticateCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<AuthDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
		{
			return await _authService.AuthenticateAsync(request, cancellationToken);
		}
	}
}
