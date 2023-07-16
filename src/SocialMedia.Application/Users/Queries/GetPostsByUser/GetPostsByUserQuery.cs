using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Users.Queries.GetPostsByUser
{
	public record GetPostsByUserQuery : IRequest<ApiPaginatedResponse<UserPostDto>>
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 50;
	}
}
