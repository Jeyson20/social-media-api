namespace SocialMedia.Domain.Entities.Posts
{
	public class Like
	{
        internal Like( int userId, int postId)
        {
            UserId = userId;
			PostId = postId;
        }
        public int UserId { get; private set; }
		public User? User { get; private set; }
		public int PostId { get; private set; }
		public Post? Post { get; private set; }
	}
}
