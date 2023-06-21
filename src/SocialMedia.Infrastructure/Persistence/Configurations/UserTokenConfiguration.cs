using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Persistence.Configurations
{
	public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder.ToTable("UserTokens");

			builder.HasKey(u => u.Id);

			builder.HasOne(t => t.User)
			.WithOne(u => u.Token)
			.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
