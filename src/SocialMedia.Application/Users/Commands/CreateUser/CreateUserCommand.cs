using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.CreateUser
{
	public record CreateUserCommand : IRequest<ApiResponse<int>>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
