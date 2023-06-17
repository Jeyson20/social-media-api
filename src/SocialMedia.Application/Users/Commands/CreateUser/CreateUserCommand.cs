using AutoMapper;
using MediatR;
using SocialMedia.Application.Common.Helpers;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<ApiResponse<int>>, IMapFrom<User>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? PhoneNumber { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CreateUserCommand, User>()
				.ForMember(des => des.Status, act => act.MapFrom(_ => Status.Active))
				.ForMember(des => des.Password, act => act.MapFrom(src => PasswordHelper.HashPassword(src.Password!)));
		}
	}
}
