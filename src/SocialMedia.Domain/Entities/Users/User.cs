using SocialMedia.Domain.Entities.Users.Events;

namespace SocialMedia.Domain.Entities.Users
{
	public class User : BaseAuditableEntity
	{
		private readonly List<Post> _posts = new();
		private readonly List<Comment> _comments = new();
		public string FirstName { get; private set; } = string.Empty;
		public string LastName { get; private set; } = string.Empty;
		public DateTime DateOfBirth { get; private set; }
		public Gender Gender { get; private set; }
		public string Email { get; private set; } = string.Empty;
		public string Username { get; private set; } = string.Empty;
		public string Password { get; private set; } = string.Empty;
		public string PhoneNumber { get; private set; } = string.Empty;
		public Status Status { get; private set; }
		public UserToken? Token { get; private set; }
		public IReadOnlyList<Post> Posts => _posts;
		public IReadOnlyList<Comment> Comments => _comments;

#pragma warning disable CS8618
		private User() { }
		public static User Create(string firstName, string lastName, DateTime dateOfBirth, Gender gender,
			string email, string username, string password, string phoneNumber
			)
		{
			var newUser = new User
			{
				FirstName = firstName,
				LastName = lastName,
				DateOfBirth = dateOfBirth,
				Gender = gender,
				Email = email,
				Username = username,
				Password = password,
				PhoneNumber = phoneNumber,
				Status = Status.Active
			};

			newUser.AddDomainEvent(new UserCreatedEvent(newUser));

			return newUser;
		}
		public void Update(string firstName, string lastName, DateTime dateOfBirth, Gender gender,
			string phoneNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			Gender = gender;
			PhoneNumber = phoneNumber;

			AddDomainEvent(new UserUpdatedEvent(Id, firstName, lastName,
				dateOfBirth, gender, phoneNumber));
		}
		public void Deactivate()
		{
			Status = Status.Inactive;
			AddDomainEvent(new UserDeactivatedEvent(Id));
		}
		public void SetUserToken(string token)
		{
			DateTime expiration = DateTime.Now.AddDays(1);
			var userToken = new UserToken(token, expiration);
			Token = userToken;

			AddDomainEvent(new UserTokenCreatedEvent(userToken));
		}
	}
}
