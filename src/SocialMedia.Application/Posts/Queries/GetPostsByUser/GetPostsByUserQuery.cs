using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetPostsByUser
{
	public record GetPostsByUserQuery : IRequest<ApiPaginatedResponse<PostDto>>
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 50;
	}
}
