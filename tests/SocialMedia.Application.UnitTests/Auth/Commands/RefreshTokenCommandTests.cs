using SocialMedia.Application.Auth.Commands.RefreshToken;
using SocialMedia.Application.Auth.DTOs;

namespace SocialMedia.Application.UnitTests.Auth.Commands;

public class RefreshTokenCommandTests
{
	[Fact]
	public async Task RefreshTokenCommandHandler_ShouldReturnAuthDto_WhenRefreshTokenIsValid()
	{
		// Arrange
		var authServiceMock = new Mock<IAuthService>();
		var cancellationToken = new CancellationToken();

		authServiceMock.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenCommand>(), cancellationToken))
			.ReturnsAsync(new TokenDto("your-access-token"));

		var commandHandler = new RefreshTokenCommandHandler(authServiceMock.Object);
		var command = new RefreshTokenCommand("testRefreshToken");

		// Act
		var result = await commandHandler.Handle(command, cancellationToken);

		// Assert
		result.Should().NotBeNull();
		result.Token.Should().Be("your-access-token");
	}

	[Fact]
	public async Task RefreshTokenCommandHandler_ShouldThrowApiException_WhenRefreshTokenIsInvalid()
	{
		// Arrange
		var authServiceMock = new Mock<IAuthService>();
		var cancellationToken = new CancellationToken();

		authServiceMock.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenCommand>(), cancellationToken))
			.ThrowsAsync(new ApiException());

		var commandHandler = new RefreshTokenCommandHandler(authServiceMock.Object);
		var command = new RefreshTokenCommand("testRefreshToken");

		// Act
		Func<Task> act = async () => await commandHandler.Handle(command, cancellationToken);

		// Assert
		await act.Should().ThrowAsync<ApiException>();
	}
}
