using MediatR;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Posts.Commands.AddComment;

public record AddCommentToPostCommand(int PostId, string Text): IRequest<ApiResponse<int>>;
