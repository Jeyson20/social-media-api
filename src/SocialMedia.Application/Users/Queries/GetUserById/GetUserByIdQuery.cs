using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Users.Queries.GetUserById
{
	public record class GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserDto>>;
}
