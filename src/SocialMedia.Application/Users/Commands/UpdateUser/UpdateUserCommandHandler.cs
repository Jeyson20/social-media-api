using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		public UpdateUserCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ApiResponse<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = _context.Users.FirstOrDefault(x => x.Id == request.Id) ??
				throw new KeyNotFoundException("User not found");

			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.DateOfBirth = request.DateOfBirth;
			user.Gender = request.Gender;
			user.PhoneNumber = request.PhoneNumber;
			user.Status = request.Status;

			_context.Users.Update(user);

			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(user.Id, "User has been updated successfully");
		}
	}
}
