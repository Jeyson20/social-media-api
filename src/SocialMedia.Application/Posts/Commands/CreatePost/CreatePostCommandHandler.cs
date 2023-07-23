using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUser;

		public CreatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		public async Task<ApiResponse<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
		{
			int userId = _currentUser.GetUserId();

			if (!_context.Users.Any(x => x.Id == userId))
				throw new KeyNotFoundException("User not found");

			var newPost = Post.Create(userId, request.Description, request.Image);

			_context.Posts.Add(newPost);

			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(newPost.Id);
		}
	}
}
