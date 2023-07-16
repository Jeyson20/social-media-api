using MediatR;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities.Users;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.UpdateUser
{
	public record UpdateUserCommand : IRequest<ApiResponse<int>>, IMapFrom<User>
	{
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
		public string LastName { get; init; } = string.Empty;
		public DateTime DateOfBirth { get; init; }
		public Gender Gender { get; init; } 
		public string PhoneNumber { get; init; } = string.Empty;
	}
}
