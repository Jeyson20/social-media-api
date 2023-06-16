using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(u => u.Id);

			builder.Property(u => u.FirstName)
				.HasMaxLength(50);

			builder.Property(u => u.LastName)
				.HasMaxLength(50);

			builder.Property(u => u.DateOfBirth)
				.IsRequired();

			builder.Property(u => u.Gender)
				.IsRequired();

			builder.Property(u => u.Email)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(u => u.Username)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(u => u.Password)
				.IsRequired();

			builder.Property(u => u.PhoneNumber)
				.HasMaxLength(20);

			builder.Property(u => u.Status)
				.IsRequired();

			builder.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
