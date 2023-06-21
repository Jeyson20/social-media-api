using MediatR;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.Auth.Commands.RefreshToken
{
	public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenDto>;
}
