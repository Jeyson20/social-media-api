using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.UnitTests.Auth.Commands;
public class AuthenticateCommandTests
{
	[Fact]
	public async Task AuthenticateCommandHandler_ShouldReturnAuthDto_WhenAuthenticationIsSuccessful()
	{
		// Arrange
		var authServiceMock = new Mock<IAuthService>();
		var cancellationToken = new CancellationToken();

		authServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<AuthenticateCommand>(), cancellationToken))
			.ReturnsAsync(new AuthDto("your-access-token", "your-refresh-token"));

		var commandHandler = new AuthenticateCommandHandler(authServiceMock.Object);
		var command = new AuthenticateCommand("testuser", "testpassword");

		// Act
		var result = await commandHandler.Handle(command, cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Token.Should().Be("your-access-token");
		result.RefreshToken.Should().Be("your-refresh-token");
	}

	[Fact]
	public async Task AuthenticateCommandHandler_Handle_ShouldThrowApiException_WhenAuthenticationFails()
	{
		// Arrange
		var authServiceMock = new Mock<IAuthService>();
		var cancellationToken = new CancellationToken();

		authServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<AuthenticateCommand>(), cancellationToken))
			.ThrowsAsync(new ApiException());

		var commandHandler = new AuthenticateCommandHandler(authServiceMock.Object);
		var command = new AuthenticateCommand("testuser", "testpassword");

		// Act
		Func<Task> act = async () => await commandHandler.Handle(command, cancellationToken);

		// Assert
		await act.Should().ThrowAsync<ApiException>();
	}
}
