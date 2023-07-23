namespace SocialMedia.Domain.Entities
{
	public class UserToken : BaseAuditableEntity
    {
        internal UserToken(int userId , string token, DateTime expiration)
        {
            UserId = userId;
            Token = token;
            Expiration = expiration;
        }
        public int UserId { get; private set; }
        public string Token { get; private set; } = null!;
        public DateTime Expiration { get; private set; }
        public User? User { get; private set; }

        public static UserToken Create(int userId , string token)
        {
			DateTime expiration  = DateTime.Now.AddDays(1);
			var newUserToken = new UserToken(userId, token, expiration);

			newUserToken.AddDomainEvent(new UserTokenCreatedEvent(newUserToken));

			return newUserToken;
        }

        public void Update(string token)
        {
            Expiration = DateTime.Now.AddDays(1);
            Token = token;
        }
    }
}
