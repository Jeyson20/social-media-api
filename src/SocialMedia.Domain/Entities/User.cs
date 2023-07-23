namespace SocialMedia.Domain.Entities
{
	public class User : BaseAuditableEntity
	{
		private readonly List<Post> _posts = new();
		private readonly List<Comment> _comments = new();
		private readonly List<Like> _likes = new();
		public string FirstName { get; private set; } = string.Empty;
		public string LastName { get; private set; } = string.Empty;
		public DateTime DateOfBirth { get; private set; }
		public Gender Gender { get; private set; }
		public string Email { get; private set; } = string.Empty;
		public string Username { get; private set; } = string.Empty;
		public string Password { get; private set; } = string.Empty;
		public string PhoneNumber { get; private set; } = string.Empty;
		public Status Status { get; private set; }
		public UserToken? RefreshToken { get; private set; }
		public IReadOnlyList<Post> Posts => _posts;
		public IReadOnlyList<Comment> Comments => _comments;
		public IReadOnlyList<Like> Likes => _likes;
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
			AddDomainEvent(new UserDeletedEvent(Id));
		}

		public void SetUserToken(string token)
		{
			var userToken = UserToken.Create(Id, token);
			RefreshToken = userToken;
		}

		public void UpdateUserToken(string token)
		{
			RefreshToken!.Update(token);
			AddDomainEvent(new UserTokenUpdatedEvent(Id, token, RefreshToken.Expiration));
		}

		public void AddUserPost(string description, string? image)
		{
			var newPost = Post.Create(Id, description, image);
			_posts.Add(newPost);
		}

		public void DeleteUserPost(int postId)
		{
			var post = _posts.FirstOrDefault(x => x.Id == postId);

			if (post is null) throw new KeyNotFoundException(nameof(post));

			_posts.Remove(post);

			AddDomainEvent(new PostDeletedEvent(Id, postId));
		}

		public void AddCommentToUserPost(int postId, string text)
		{
			var newComment = Comment.Create(Id, postId, text);
			_comments.Add(newComment);

			var post = _posts.FirstOrDefault(x => x.Id == postId);

			if (post is null) throw new KeyNotFoundException(nameof(post));

			post.AddComment(newComment.UserId, postId, text);
		}

		public void RemoveUserComment(int postId,int commentId)
		{
			var post = _posts.FirstOrDefault(x => x.Id == postId);

			if (post is null) throw new KeyNotFoundException(nameof(post));

			var comment = _comments.FirstOrDefault(x => x.Id == commentId);

			if (comment is null) throw new KeyNotFoundException(nameof(comment));

			post.RemoveComment(commentId);
			_comments.Remove(comment);
		}

		public void AddLikeToUserPost(int postId)
		{
			var post = _posts.FirstOrDefault(x => x.Id == postId);
			if (post is null) throw new KeyNotFoundException(nameof(post));

			post.AddLike(Id, postId);
			_likes.Add(post.Likes[0]);
		}

		public void RemoveLikeToUserPost(int postId, int likeId)
		{
			var post = _posts.FirstOrDefault(x => x.Id == postId);
			if (post is null) throw new KeyNotFoundException(nameof(post));

			var like = _likes.FirstOrDefault(x => x.Id == likeId);
			if (like is null) throw new KeyNotFoundException(nameof(like));

			post.RemoveLike(likeId);
			_likes.Remove(like);
		}

	}
}
