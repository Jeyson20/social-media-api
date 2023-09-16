using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Posts.Commands.AddComment
{
	public class AddCommentToPostCommandHandler : IRequestHandler<AddCommentToPostCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;

		public AddCommentToPostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}

		public async Task<ApiResponse<int>> Handle(AddCommentToPostCommand request, CancellationToken cancellationToken)
		{
			var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken)
				?? throw new KeyNotFoundException("Post not found");

			post.AddComment(_currentUserService.GetUserId(), request.PostId, request.Text);

			await _context.SaveChangesAsync(cancellationToken);

			return ApiResponse<int>.Success(post.Comments[0].Id);

		}
	}
}
