namespace SocialMedia.Domain.Entities
{
	public class User : BaseAuditableEntity
	{
		private User() { }
		private User(string? firstName, string? lastName, DateTime dateOfBirth, Gender gender,
		string? email, string? username, string? password, string? phoneNumber, Status status
		)
		{
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			Gender = gender;
			Email = email;
			Username = username;
			Password = password;
			PhoneNumber = phoneNumber;
			Status = status;
		}

		public string? FirstName { get; private set; }
		public string? LastName { get; private set; }
		public DateTime DateOfBirth { get; private set; }
		public Gender Gender { get; private set; }
		public string? Email { get; private set; }
		public string? Username { get; private set; }
		public string? Password { get; private set; }
		public string? PhoneNumber { get; private set; }
		public Status Status { get; private set; }
		public UserToken? Token { get; private set; }
		public ICollection<Post>? Posts { get; private set; }
		public ICollection<Comment>? Comments { get; private set; }

		public static User Create(string? firstName, string? lastName, DateTime dateOfBirth, Gender gender,
			string? email, string? username, string? password, string? phoneNumber
			)
		{
			return new User(firstName, lastName, dateOfBirth, gender,
				email, username, password, phoneNumber, Status.Active
			);
		}
		public void Update(string? firstName, string? lastName, DateTime dateOfBirth, Gender gender,
			string? phoneNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			Gender = gender;
			PhoneNumber = phoneNumber;
		}
		public void Deactivate()
		{
			Status = Status.Inactive;
		}
		public void SetUserToken(string token)
		{
			DateTime expiration = DateTime.Now.AddDays(1);
			Token = new UserToken(token, expiration);
		}
	}
}
