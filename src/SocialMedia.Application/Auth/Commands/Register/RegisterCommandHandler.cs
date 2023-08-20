using MediatR;
using SocialMedia.Application.Common.Helpers;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<int>>
    {
        private readonly IApplicationDbContext _context;
        public RegisterCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = PasswordHelper.HashPassword(request.Password!);

            var newUser = User.Create(request.FirstName, request.LastName, request.DateOfBirth,
                request.Gender, request.Email, request.Username,
                passwordHash, request.PhoneNumber);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            return ApiResponse<int>.Success(newUser.Id);
        }
    }
}
