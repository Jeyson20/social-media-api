using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Auth.Queries.Profile
{
	public record ProfileQuery() : IRequest<ApiResponse<UserDto>>;
}
