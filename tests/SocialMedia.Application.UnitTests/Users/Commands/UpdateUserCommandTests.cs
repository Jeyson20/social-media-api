using SocialMedia.Application.Users.Commands.UpdateUser;

namespace SocialMedia.Application.UnitTests.Users.Commands;

public class UpdateUserCommandTests
{
    private readonly ApplicationDbContextMock _contextMock;
    public UpdateUserCommandTests()
    {
        _contextMock = new();
    }

    private static ICollection<User> GetUsersData()
    {
        return new List<User>
        {
            User.Create("test", "test", new DateTime(1990, 1, 1), Gender.Male,
            "test@test.com","test", "test", "test"),
        };
    }

    [Fact]
    public async Task UpdateUserCommandHandler_ShouldUpdateUserAndReturnApiResponseWithUserId_WhenTheUserExist()
    {
        // Arrange
        int userId = 0;
        var cancellationToken = new CancellationToken();

        var request = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Gender.Male,
            PhoneNumber = "1234567890"
        };

        var usersDbSet = _contextMock.MockDbSet(GetUsersData());
        _contextMock.Setup(x => x.Users).Returns(usersDbSet);

        var commandHandler = new UpdateUserCommandHandler(_contextMock.Object);

        // Act
        var result = await commandHandler.Handle(request, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(userId);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateUserCommandHandler_ShouldThrowKeyNotFoundException_WhenTheUserIsNotFound()
    {
        // Arrange
        int userId = 1;
        var cancellationToken = new CancellationToken();

        var request = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Gender.Male,
            PhoneNumber = "1234567890"
        };

        var usersDbSet = _contextMock.MockDbSet(GetUsersData());
        _contextMock.Setup(x => x.Users).Returns(usersDbSet);

        var commandHandler = new UpdateUserCommandHandler(_contextMock.Object);

        // Act
        Func<Task> act = async () => await commandHandler.Handle(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

}
