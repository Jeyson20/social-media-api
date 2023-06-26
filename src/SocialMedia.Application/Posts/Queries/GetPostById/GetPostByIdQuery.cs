using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetPostById
{
	public record GetPostByIdQuery(int Id) : IRequest<ApiResponse<PostDto>>;
}
