using MediatR;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommand : IRequest<ApiResponse<int>>, IMapFrom<User>
	{
        public int Id { get; init; }
        public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public DateTime DateOfBirth { get; init; }
		public Gender Gender { get; init; }
		public string? Email { get; init; }
		public string? PhoneNumber { get; init; }
		public Status Status { get; init; }
	}
}
