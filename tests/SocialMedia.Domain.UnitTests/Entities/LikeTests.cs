using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.UnitTests.Entities;

public class LikeTests
{
    [Fact]
    public void Create_ShouldAddALike()
    {
        //Arrange
        int userId = 1;
        int postId = 1;

        //Act
        var newLike = Like.Create(userId, postId);

        //Assert
        newLike.Should().NotBeNull();
        newLike.UserId.Should().Be(userId);
        newLike.PostId.Should().Be(postId);

    }
}
