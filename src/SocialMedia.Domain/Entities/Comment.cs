namespace SocialMedia.Domain.Entities
{
	public class Comment : BaseAuditableEntity
	{
		internal Comment(int userId, int postId, string text)
		{
			UserId = userId;
			PostId = postId;
			Text = text;
		}
		public int PostId { get; private set; }
		public int UserId { get; private set; }
		public string? Text { get; private set; }
		public Post? Post { get; private set; }
		public User? User { get; private set; }

		public static Comment Create(int userId, int postId, string text)
		{
			var newComment = new Comment(userId, postId, text);

			newComment.AddDomainEvent(new CommentCreatedEvent(newComment));
			return newComment;
		}
	}
}
