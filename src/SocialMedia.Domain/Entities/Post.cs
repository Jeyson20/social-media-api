namespace SocialMedia.Domain.Entities
{
	public class Post : BaseAuditableEntity
	{
		public int UserId { get; set; }
		public User User { get; set; } = null!;
		public string? Description { get; set; }
		public string? Image { get; set; }
		public List<Comment>? Comments { get; private set; }
		public ICollection<Like>? Likes { get; private set; }

	}
}
