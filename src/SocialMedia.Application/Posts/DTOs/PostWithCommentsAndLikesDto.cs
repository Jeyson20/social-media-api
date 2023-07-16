using AutoMapper;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities.Posts;

namespace SocialMedia.Application.Posts.DTOs
{
    public record PostWithCommentsAndLikesDto : IMapFrom<Post>
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Description { get; set; }
		public int Comments { get; set; }
		public int Likes { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Post, PostWithCommentsAndLikesDto>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments!.Count))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes!.Count));
		}
	}

	public record PostCommentDto
	{
		public int Id { get; set; }
		public string? Text { get; set; }
		public string? Username { get; set; }
	}
}
