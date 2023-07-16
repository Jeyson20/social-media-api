namespace SocialMedia.Domain.Entities.Users
{
	public class UserToken : BaseAuditableEntity
    {
        internal UserToken(string? token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        public int UserId { get; private set; }
        public string? Token { get; private set; }
        public DateTime Expiration { get; private set; }
        public User? User { get; private set; }

        public void UpdateUsertoken(string token)
        {
            Expiration = DateTime.Now.AddDays(1);
            Token = token;
        }
    }
}
