using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.UnitTests.Entities;
public class PostTests
{
    [Fact]
    public void Create_ShouldCreateAPost()
    {
        //Arrange
        int userId = 1;
        string description = "test";
        string image = "test.jpg";

        //Act
        var post = Post.Create(userId, description, image);

        //Assert
        post.Should().NotBeNull();
        post.UserId.Should().Be(userId);
        post.Description.Should().Be(description);
        post.Image.Should().Be(image);
    }

    [Fact]
    public void AddComment_ShouldAddACommentToPost()
    {
        //Arrange
        int userId = 1;
        string description = "test";
        string image = "test.jpg";
        var post = Post.Create(userId, description, image);
        string text = "test";

        //Act
        post.AddComment(userId, post.Id, text);

        //Assert
        post.Comments.Should().NotBeEmpty();
    }

    [Fact]
    public void RemoveComment_ShouldRemoveACommentFromPost()
    {
        //Arrange
        int userId = 1;
        int commentId = 0;
        var post = Post.Create(userId, "test", "test");
        post.AddComment(userId, post.Id, "test");

        //Act
        post.RemoveComment(commentId);

        //Assert
        post.Comments.Should().BeEmpty();
    }

    [Fact]
    public void RemoveComment_ShouldThrowAKeyNotFoundException_WhenTheCommentIsNotFound()
    {
        //Arrange
        int userId = 1;
        int commentId = 1;
        var post = Post.Create(userId, "test", "test");
        post.AddComment(userId, post.Id, "test");

        //Act
        Action action = () => post.RemoveComment(commentId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void AddLike_ShouldAddALikeToPost()
    {
        //Arrange
        int userId = 1;
        string description = "test";
        string image = "test.jpg";
        var post = Post.Create(userId, description, image);

        //Act
        post.AddLike(userId, post.Id);

        //Assert
        post.Likes.Should().NotBeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldRemoveALikeFromPost()
    {
        //Arrange
        int userId = 1;
        int likeId = 0;
        var post = Post.Create(userId, "test", "test.jpg");
        post.AddLike(userId, post.Id);

        //Act
        post.RemoveLike(likeId);

        //Assert
        post.Likes.Should().BeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldThrowAKeyNotFoundException_WhenTheLikeIsNotFound()
    {
        //Arrange
        int userId = 1;
        int likeId = 1;
        var post = Post.Create(userId, "test", "test.jpg");
        post.AddLike(userId, post.Id);

        //Act
        Action action = () => post.RemoveLike(likeId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }
}
