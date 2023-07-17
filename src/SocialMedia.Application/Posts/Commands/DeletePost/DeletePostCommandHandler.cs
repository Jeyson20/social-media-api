using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Posts.Commands.DeletePost
{
	internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;

		public DeletePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}

		public async Task<ApiResponse<int>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.GetUserId();

			var post = await _context.Posts
				.Include(x=>x.User)
				.Where(x => x.Id == request.Id && x.UserId == userId)
				.FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException("Post not found");

			post.User.DeleteUserPost(request.Id);

			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(request.Id);
		}
	}
}
