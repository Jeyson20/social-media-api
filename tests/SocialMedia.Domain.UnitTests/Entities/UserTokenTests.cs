using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.UnitTests.Entities;
public class UserTokenTests
{
    [Fact]
    public void CreateUserToken_ShouldCreateAUserToken()
    {
        //Arrange
        int userId = 1;
        string token = "sampleToken";

        //Act
        var userToken = UserToken.Create(userId, token);

        //Assert
        userToken.Should().NotBeNull();
        userToken.UserId.Should().Be(userId);
        userToken.Token.Should().Be(token);
    }
    [Fact]
    public void UpdateUserToken_ShouldUpdateAUserToken()
    {
        //Arrange
        string newToken = "sampleToken";
        var userToken = UserToken.Create(1, "sample");

        //Act
        userToken.Update(newToken);

        //Assert
        userToken.Token.Should().Be(newToken);
    }
}
