using AutoMapper;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Users.DTOs
{
    public record UserPostDto : IMapFrom<Post>
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Description { get; set; }
		public int Comments { get; set; }
		public int Likes { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Post, UserPostDto>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Count))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count));
		}
	}
}
