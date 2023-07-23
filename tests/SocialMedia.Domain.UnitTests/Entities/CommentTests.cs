using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.UnitTests.Entities;

public class CommentTests
{
    [Fact]
    public void Create_ShouldCreateANewComment()
    {
        //Arrange
        int userId = 1;
        int postId = 1;
        string text = "test";

        //Act
        var newComment = Comment.Create(userId, postId, text);

        //Assert
        newComment.Should().NotBeNull();
        newComment.UserId.Should().Be(userId);
        newComment.PostId.Should().Be(postId);
        newComment.Text.Should().Be(text);
    }

}
