using MediatR;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Posts.Commands.DeletePost
{
	public record DeletePostCommand(int Id) : IRequest<ApiResponse<int>>;

}
