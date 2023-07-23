using SocialMedia.Application.Auth.Queries.Profile;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.UnitTests.Auth.Queries;

public class ProfileQueryTests
{
	private readonly ApplicationDbContextMock _dbContextMock;
	private readonly Mock<ICurrentUserService> _currentUserMock;
	private readonly Mock<IMapper> _mapperMock;
	public ProfileQueryTests()
	{
		_dbContextMock = new();
		_currentUserMock = new();
		_mapperMock = new();
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
	public async Task ProfileQueryHandler_ShouldReturnUserDto_WhenTheUserExists()
	{
		// Arrange
		var userId = 0;
		var cancellationToken = new CancellationToken();
		var usersData = GetUsersData();

		_dbContextMock.Setup(x => x.Users).Returns(_dbContextMock.MockDbSet(usersData));
		_currentUserMock.Setup(x => x.GetUserId()).Returns(userId);
		_mapperMock.Setup(x => x.ConfigurationProvider).Returns(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()));

		var queryHandler = new ProfileQueryHandler(_currentUserMock.Object, _dbContextMock.Object, _mapperMock.Object);
		var query = new ProfileQuery();

		// Act
		var result = await queryHandler.Handle(query, cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result?.Data?.Id.Should().Be(userId);
		result?.Message.Should().Be("OK");
	}

	[Fact]
	public async Task ProfileQueryHandler_ShouldThrowKeyNotFoundException_WhenTheUserDoesNotExist()
	{
		// Arrange
		var userId = 1;
		var cancellationToken = new CancellationToken();
		var usersData = GetUsersData();

		_dbContextMock.Setup(x => x.Users).Returns(_dbContextMock.MockDbSet(usersData));
		_currentUserMock.Setup(x => x.GetUserId()).Returns(userId);
		_mapperMock.Setup(x => x.ConfigurationProvider).Returns(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()));

		var queryHandler = new ProfileQueryHandler(_currentUserMock.Object, _dbContextMock.Object, _mapperMock.Object);
		var query = new ProfileQuery();

		// Act
		Func<Task> act = async () => await queryHandler.Handle(query, cancellationToken);

		// Assert
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
