namespace SocialMedia.Domain.Entities
{
	public class User : BaseAuditableEntity
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
        public  Gender Gender { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
		public Status Status { get; set; }
		public ICollection<Post> Posts { get; private set; } = new List<Post>();
		public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
	}
}
