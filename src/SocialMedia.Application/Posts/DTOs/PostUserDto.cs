using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Posts.DTOs
{
	public record PostUserDto : IMapFrom<Post>
	{
        public int Id { get; set; }
        public string? Description { get; set; }
		public string? Image { get; set; }
	}
}
