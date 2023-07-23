using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Auth.Commands.Register
{
    public record RegisterCommand : IRequest<ApiResponse<int>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
