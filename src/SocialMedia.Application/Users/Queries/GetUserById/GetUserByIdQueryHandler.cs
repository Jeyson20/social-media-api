using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Users.Queries.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var userDto = await _context.Users
				.Where(x => x.Id == request.Id)
				.AsNoTracking()
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken)
				?? throw new KeyNotFoundException("User not found");

			return ApiResponse<UserDto>.Success(userDto);
		}
	}
}
