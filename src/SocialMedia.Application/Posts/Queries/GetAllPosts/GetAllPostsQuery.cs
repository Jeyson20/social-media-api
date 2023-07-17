using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetAllPosts
{
	public record GetAllPostsQuery : PaginatedRequest, IRequest<ApiPaginatedResponse<PostDto>>;
}