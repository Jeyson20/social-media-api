using AutoMapper;
using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<int>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var newUser = _mapper.Map<User>(request);

			_context.Users.Add(newUser);
			await _context.SaveChangesAsync(cancellationToken);

			return new ApiResponse<int>(newUser.Id);
		}
	}
}
