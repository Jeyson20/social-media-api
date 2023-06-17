using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Users.Queries.GetUsers
{
	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiPaginatedResponse<UserDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiPaginatedResponse<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var specification = new UserSpecification(
				status: request.Status,
				username: request.Username,
				email: request.Email
				);

			return await _context.Users
				.WithSpecification(specification)
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
		}
	}
}
