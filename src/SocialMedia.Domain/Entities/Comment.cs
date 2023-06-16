namespace SocialMedia.Domain.Entities
{
	public class Comment : BaseAuditableEntity
	{
		public int PostId { get; set; }
		public int UserId { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }
		public Post Post { get; set; } = null!;
		public User User { get; set; } = null!;
	}
}
