using MediatR;
using SocialMedia.Application.Common.Helpers;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities.Users;

namespace SocialMedia.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		public CreateUserCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ApiResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var passwordHash = PasswordHelper.HashPassword(request.Password!);

			var newUser = User.Create(request.FirstName, request.LastName, request.DateOfBirth,
				request.Gender, request.Email, request.Username,
				passwordHash, request.PhoneNumber);

			_context.Users.Add(newUser);
			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(newUser.Id);
		}
	}
}
