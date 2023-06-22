using AutoMapper;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Posts.DTOs
{
	public record PostWithCommentsAndLikesDto : IMapFrom<Post>
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Description { get; set; }
		public ICollection<PostCommentDto>? Comments { get; set; }
		public int Likes { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Post, PostWithCommentsAndLikesDto>()
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count));

			profile.CreateMap<Comment, PostCommentDto>()
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.Username));
		}
	}

	public record PostCommentDto
	{
		public int Id { get; set; }
		public string? Description { get; set; }
		public string? Username { get; set; }
	}
}
