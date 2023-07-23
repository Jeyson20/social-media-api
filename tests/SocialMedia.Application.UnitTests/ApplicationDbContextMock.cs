using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

namespace SocialMedia.Application.UnitTests;

public class ApplicationDbContextMock : Mock<IApplicationDbContext>
{
	private readonly List<object> _dbSets = new();

	public ApplicationDbContextMock(MockBehavior behavior = MockBehavior.Strict)
		: base(behavior)
	{
		SetupSaveChangesAsync();
	}

	public DbSet<TEntity> MockDbSet<TEntity>(ICollection<TEntity> data) where TEntity : class
	{
		var mockDbSet = data.AsQueryable().BuildMockDbSet();
		_dbSets.Add(mockDbSet);
		return mockDbSet.Object;
	}

	public DbSet<TEntity> GetMockDbSet<TEntity>() where TEntity : class
			#pragma warning disable CS8603 
		=> _dbSets.OfType<DbSet<TEntity>>().FirstOrDefault();

	private void SetupSaveChangesAsync() // Verify that the change was saved in the context
		=> Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

	private static Mock<IApplicationDbContext> SetUp()
	{
		var mock = new Mock<IApplicationDbContext>();
		mock.Setup(m => m.Users).Returns(new Mock<DbSet<User>>().Object);
		mock.Setup(m => m.UserTokens).Returns(new Mock<DbSet<UserToken>>().Object);
		mock.Setup(m => m.Comments).Returns(new Mock<DbSet<Comment>>().Object);
		mock.Setup(m => m.Posts).Returns(new Mock<DbSet<Post>>().Object);
		mock.Setup(m => m.Likes).Returns(new Mock<DbSet<Like>>().Object);
		return mock;
	}

}