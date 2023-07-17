using AutoMapper;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities.Posts;

namespace SocialMedia.Application.Posts.DTOs
{
    public record PostDto : IMapFrom<Post>
	{
		public int Id { get; set; }
        public string? Username { get; set; }
        public string? Image { get; set; }
		public string? Description { get; set; }
		public int Comments { get; set; }
		public int Likes { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Post, PostDto>()
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Count))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count));
		}
	}
}
