using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Persistence.Configurations
{
	public class LikeConfiguration : IEntityTypeConfiguration<Like>
	{
		public void Configure(EntityTypeBuilder<Like> builder)
		{
			builder.ToTable("Likes");

			builder.HasKey(x => x.Id);

			builder.HasAlternateKey(x => new { x.UserId, x.PostId });

			builder.HasOne(x => x.Post)
				.WithMany(x => x.Likes)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(x => x.User)
				.WithMany(x => x.Likes)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
