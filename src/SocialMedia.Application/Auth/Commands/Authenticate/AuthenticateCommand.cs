using MediatR;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.Auth.Commands.Authenticate
{
	public record AuthenticateCommand(string Username, string Password) : IRequest<TokenDto>;
}
