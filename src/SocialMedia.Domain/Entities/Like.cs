namespace SocialMedia.Domain.Entities
{
	public class Like : BaseAuditableEntity
	{
		internal Like(int userId, int postId)
		{
			UserId = userId;
			PostId = postId;
		}
		public int UserId { get; private set; }
		public User? User { get; private set; }
		public int PostId { get; private set; }
		public Post? Post { get; private set; }

		public static Like Create(int userId, int postId)
		{
			var newLike = new Like(userId, postId);

			newLike.AddDomainEvent(new LikeCreatedEvent(newLike));

			return newLike;
		}
	}
}
