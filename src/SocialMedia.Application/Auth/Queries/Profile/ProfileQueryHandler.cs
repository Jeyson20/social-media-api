using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Auth.Queries.Profile
{
	public class ProfileQueryHandler : IRequestHandler<ProfileQuery, ApiResponse<UserDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;

		public ProfileQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext context, IMapper mapper)
		{
			_currentUserService = currentUserService;
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiResponse<UserDto>> Handle(ProfileQuery request, CancellationToken cancellationToken)
		{
			int? userId = _currentUserService.GetUserId();

			var user = await _context.Users
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
				?? throw new KeyNotFoundException("User not found");

			return new ApiResponse<UserDto>(user);

		}
	}
}
