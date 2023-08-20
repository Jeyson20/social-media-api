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

			user.Update(request.FirstName, request.LastName,
				request.DateOfBirth, request.Gender, request.PhoneNumber);

			_context.Users.Update(user);

			await _context.SaveChangesAsync(cancellationToken);

			return ApiResponse<int>.Success(user.Id, "User has been updated successfully");
		}
	}
}
