using SocialMedia.Domain.Entities.Users;

namespace SocialMedia.Domain.Entities
{
	public class Comment : BaseAuditableEntity
	{
		public int PostId { get; set; }
		public int UserId { get; set; }
		public string? Text { get; set; }
		public Post? Post { get; set; }
		public User? User { get; set; }
	}
}
