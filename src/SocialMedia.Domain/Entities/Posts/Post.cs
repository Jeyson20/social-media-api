using SocialMedia.Domain.Entities.Posts.Events;

namespace SocialMedia.Domain.Entities.Posts
{
	public class Post : BaseAuditableEntity
	{
		private readonly List<Comment> _comments = new();
		private readonly List<Like> _likes = new();
		public int UserId { get; private set; }
		public User User { get; private set; } = null!;
		public string? Description { get; private set; }
		public string? Image { get; private set; }
		public IReadOnlyList<Like> Likes => _likes;
		public IReadOnlyList<Comment> Comments => _comments;

		private Post() { }
		public static Post Create(int userId, string? description, string? image)
		{
			var newPost = new Post
			{
				UserId = userId,
				Description = description,
				Image = image
			};

			newPost.AddDomainEvent(new PostCreatedEvent(newPost));

			return newPost;
		}

		public void AddCommentToPost(int userId, int postId, string text)
		{
			var newComment = new Comment(userId, postId, text);
			_comments.Add(newComment);

			AddDomainEvent(new PostCommentAddedEvent(newComment));
		}

		public void AddLikeToPost(int userId, int postId)
		{
			var newLike = new Like(userId, postId);
			_likes.Add(newLike);

			AddDomainEvent(new PostLikeAddedEvent(newLike)); ;
		}

	}
}
