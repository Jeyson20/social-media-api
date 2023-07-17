using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities.Posts;
using SocialMedia.Domain.Entities.Users;

namespace SocialMedia.Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
	{
		DbSet<User> Users { get; }
		DbSet<UserToken> UserTokens { get; }
		DbSet<Post> Posts { get; }
		DbSet<Comment> Comments { get; }
		DbSet<Like> Likes { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
