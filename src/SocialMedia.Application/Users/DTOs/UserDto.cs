using AutoMapper;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Application.Users.DTOs
{
	public record UserDto : IMapFrom<UserDto>
	{
        public int Id { get; set; }
        public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? Username { get; set; }
		public string? PhoneNumber { get; set; }
		public Status Status { get; set; }
		public DateTime Created { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User,UserDto>();
		}
	}
}
