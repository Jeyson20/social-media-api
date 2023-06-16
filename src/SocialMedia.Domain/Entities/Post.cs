namespace SocialMedia.Domain.Entities
{
	public class Post : BaseEntity
	{
		public int UserId { get; set; }
		public User User { get; set; } = null!;
		public DateTime Date { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public virtual ICollection<Comment> Comments { get; private set; } = new List<Comment>();
		public virtual ICollection<Like> Likes { get; private set; } = new List<Like>();

	}
}
