using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Persistence.Configurations
{
	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.ToTable("Posts");

			builder.HasKey(d => d.Id);

			builder.Property(e => e.UserId)
				.IsRequired();

			builder.Property(e => e.Description)
				.HasMaxLength(200);

			builder.Property(e => e.Image);

			builder.HasMany(u => u.Comments)
				.WithOne(p => p.Post)
				.HasForeignKey(p => p.UserId);

			builder.HasMany(u => u.Likes)
				.WithOne(p => p.Post)
				.HasForeignKey(p => p.UserId);
		}
	}
}
