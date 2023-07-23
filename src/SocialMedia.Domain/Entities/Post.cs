namespace SocialMedia.Domain.Entities
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
		public static Post Create(int userId, string description, string? image)
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

		public void AddComment(int userId, int postId, string text)
		{
			var newComment = Comment.Create(userId, postId, text);
			_comments.Add(newComment);
		}

		public void RemoveComment(int commentId)
		{
			var comment = _comments.FirstOrDefault(x => x.Id == commentId);

			if (comment is null) throw new KeyNotFoundException(nameof(comment));

			_comments.Remove(comment);

			AddDomainEvent(new CommentDeletedEvent(commentId));
		}

		public void AddLike(int userId, int postId)
		{
			var newLike = Like.Create(userId, postId);
			_likes.Add(newLike);
		}

		public void RemoveLike(int likeId)
		{
			var like = _likes.FirstOrDefault(x => x.Id == likeId);

			if (like is null) throw new KeyNotFoundException(nameof(like));

			_likes.Remove(like);

			AddDomainEvent(new LikeDeletedEvent(likeId)); ;
		}

	}
}
