namespace SocialMedia.Domain.Entities
{
	public class Like : BaseAuditableEntity
	{
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
