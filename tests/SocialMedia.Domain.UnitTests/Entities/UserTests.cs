using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Domain.UnitTests.Entities;
public class UserTests
{
    [Fact]
    public void CreateUser_ShouldCreateANewUserWithCorrectProperties()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var gender = Gender.Male;
        var email = "john.doe@example.com";
        var username = "johndoe";
        var password = "mysecretpassword";
        var phoneNumber = "1234567890";

        // Act
        var newUser = User.Create(firstName, lastName, dateOfBirth, gender,
            email, username, password, phoneNumber);

        // Assert
        newUser.Should().NotBeNull();
        newUser.FirstName.Should().Be(firstName);
        newUser.LastName.Should().Be(lastName);
        newUser.DateOfBirth.Should().Be(dateOfBirth);
        newUser.Gender.Should().Be(gender);
        newUser.Email.Should().Be(email);
        newUser.Username.Should().Be(username);
        newUser.Password.Should().Be(password);
        newUser.PhoneNumber.Should().Be(phoneNumber);
        newUser.Status.Should().Be(Status.Active);
    }

    [Fact]
    public void UpdateUser_ShouldUpdateAUserWithNewValues()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");
        var firstName = "UpdatedFirstName";
        var lastName = "UpdatedLastName";
        var dateOfBirth = new DateTime(1985, 5, 5);
        var gender = Gender.Female;
        var phoneNumber = "9876543210";

        // Act
        user.Update(firstName, lastName, dateOfBirth, gender, phoneNumber);

        // Assert
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
        user.DateOfBirth.Should().Be(dateOfBirth);
        user.Gender.Should().Be(gender);
        user.PhoneNumber.Should().Be(phoneNumber);
    }

    [Fact]
    public void DeactivateUser_ShouldSetAUserStatusToInactive()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        // Act
        user.Deactivate();

        // Assert
        user.Status.Should().Be(Status.Inactive);

    }

    [Fact]
    public void SetUserToken_ShouldCreateANewTokenAndExpiration()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");
        var token = "sampleToken";

        // Act
        user.SetUserToken(token);

        // Assert
        user.RefreshToken.Should().NotBeNull();
        user?.RefreshToken?.Token.Should().Be(token);
        user?.RefreshToken?.Expiration.Should().BeAfter(DateTime.Now);
    }

    [Fact]
    public void UpdateUserToken_ShouldUpdateATokenAndExpiration()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");
        var token = "sampleToken";
        var newToken = "sampleToken1";

        // Act
        user.SetUserToken(token);
        user.UpdateUserToken(newToken);

        // Assert
        user.RefreshToken.Should().NotBeNull();
        user?.RefreshToken?.Token.Should().NotBe(token);
        user?.RefreshToken?.Token.Should().Be(newToken);
        user?.RefreshToken?.Expiration.Should().BeAfter(DateTime.Now);
    }

    [Fact]
    public void AddUserPost_ShouldAddAUserPost()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");
        string? description = "hello";
        string? image = "image.jpg";

        // Act
        user.AddUserPost(description, image);

        // Assert
        user.Posts.Should().NotBeEmpty();
    }

    [Fact]
    public void DeleteUserPost_ShouldRemoveAUserPost()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        string? description = "hello";
        string? image = "image.jpg";
        int postId = 0;

        // Act
        user.AddUserPost(description, image);
        user.DeleteUserPost(postId);


        // Assert
        user.Posts.Should().BeEmpty();
    }

    [Fact]
    public void DeleteUserPost_ShouldThrowAKeyNotFoundException_WhenTheUserPostIsNotFound()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        Post.Create(user.Id, "test", "test");

        // Act 
        Action action = () => user.DeleteUserPost(1);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void AddCommentToUserPost_ShouldAddACommentToTheUserPost()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        user.AddUserPost("test", "test");
        int postId = 0;

        // Act
        user.AddCommentToUserPost(postId, "test");


        // Act and Assert
        user.Posts[0].Comments.Should().NotBeEmpty();
    }

    [Fact]
    public void RemoveUserComment_ShouldBeEmpty_WhenRemoveTheComment()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        user.AddUserPost("test", "test");
        int postId = 0;
        user.AddCommentToUserPost(postId, "test");
        int commentId = 0;

        // Act
        user.RemoveUserComment(postId, commentId);

        // Act and Assert
        user.Comments.Should().BeEmpty();
    }

    [Fact]
    public void RemoveUserComment_ShouldThrowAKeyNotFoundException_WhenTheUserCommentIsNotFound()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        user.AddUserPost("test", "test");
        int postId = 0;
        user.AddCommentToUserPost(postId, "test");
        int commentId = 1;

        // Act 
        Action action = () => user.RemoveUserComment(postId, commentId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void AddLikeToUserPost_ShouldAddALikeToTheUserPost()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        user.AddUserPost("test", "test");
        int postId = 0;

        // Act 
        user.AddLikeToUserPost(postId);

        //Assert
        user.Likes.Should().NotBeEmpty();
    }

    [Fact]
    public void AddLikeToUserPost_ShouldThrowAKeyNotFoundException_WhenTheUserPostIsNotFound()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        user.AddUserPost("test", "test");
        int postId = 1;

        // Act 
        Action action = () => user.AddLikeToUserPost(postId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void RemoveLikeToUserPost_ShouldRemoveALikeFromTheUserPost()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        int postId = 0;
        int likeId = 0;
        user.AddUserPost("test", "test");
        user.AddLikeToUserPost(postId);

        // Act 
        user.RemoveLikeToUserPost(postId, likeId);

        //Assert
        user.Likes.Should().BeEmpty();

    }

    [Fact]
    public void RemoveLikeToUserPost_ShouldThrowAKeyNotFoundException_WhenThePostIsNotFound()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        int postId = 0;
        int likeId = 0;
        user.AddUserPost("test", "test");
        user.AddLikeToUserPost(postId);

        // Act 
        Action action = () => user.RemoveLikeToUserPost(1, likeId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void RemoveLikeToUserPost_ShouldThrowAKeyNotFoundException_WhenTheLikeIsNotFound()
    {
        // Arrange
        var user = User.Create("John", "Doe", new DateTime(1990, 1, 1), Gender.Male, "john.doe@example.com",
            "johndoe", "mysecretpassword", "1234567890");

        int postId = 0;
        int likeId = 1;
        user.AddUserPost("test", "test");
        user.AddLikeToUserPost(postId);

        // Act 
        Action action = () => user.RemoveLikeToUserPost(postId, likeId);

        //Assert
        action.Should().Throw<KeyNotFoundException>();
    }
}
