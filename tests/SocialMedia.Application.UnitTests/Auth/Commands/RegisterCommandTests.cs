using SocialMedia.Application.Auth.Commands.Register;

namespace SocialMedia.Application.UnitTests.Auth.Commands;

public class RegisterCommandTests
{
	private readonly ApplicationDbContextMock _contextMock;
	public RegisterCommandTests()
	{
		_contextMock = new();
	}

	[Fact]
	public async Task RegisterCommandHandler_ShouldCreateUserAndReturnApiResponseWithUserId()
	{
		// Arrange
		int userId = 0;
		var cancellationToken = new CancellationToken();

		var createUserCommand = new RegisterCommand
		{
			FirstName = "John",
			LastName = "Doe",
			DateOfBirth = new DateTime(1990, 1, 1),
			Gender = Gender.Male,
			Username = "johndoe",
			Password = "mysecretpassword",
			PhoneNumber = "1234567890"
		};

		var usersDbSet = _contextMock.MockDbSet(Array.Empty<User>());
		_contextMock.Setup(x => x.Users).Returns(usersDbSet);

		var commandHandler = new RegisterCommandHandler(_contextMock.Object);

		// Act
		var result = await commandHandler.Handle(createUserCommand, cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Data.Should().Be(userId);
		_contextMock.Verify(x => x.Users.Add(It.IsAny<User>()), Times.Once);
		_contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
	}
}
