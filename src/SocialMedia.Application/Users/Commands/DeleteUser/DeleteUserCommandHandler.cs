using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Commands.DeleteUser
{
	internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;

		public DeleteUserCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ApiResponse<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = _context.Users.FirstOrDefault(x => x.Id == request.Id)
				?? throw new KeyNotFoundException("User not found");

			user.Deactivate();

			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(user.Id);
		}
	}
}
