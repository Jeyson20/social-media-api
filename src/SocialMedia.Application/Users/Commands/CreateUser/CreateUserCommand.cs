using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.CreateUser
{
	public record CreateUserCommand : IRequest<ApiResponse<int>>
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; } 
		public Gender Gender { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } =string.Empty;
	}
}
