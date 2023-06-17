using MediatR;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Users.Commands.DeleteUser
{
	public record DeleteUserCommand(int Id) : IRequest<ApiResponse<int>>;
}
