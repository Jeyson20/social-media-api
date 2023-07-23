using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comments");

			builder.HasKey(c => c.Id);

			builder.Property(c => c.UserId)
				.IsRequired();

			builder.Property(c => c.Text)
				.IsRequired();
		}
	}
}
