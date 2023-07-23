using Ardalis.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.Application.Users.Queries.GetPostsByUser
{
	public class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQuery, ApiPaginatedResponse<UserPostDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public GetPostsByUserQueryHandler(IApplicationDbContext context, IMapper mapper,
			ICurrentUserService currentUserService)
		{
			_context = context;
			_mapper = mapper;
			_currentUserService = currentUserService;
		}

		public async Task<ApiPaginatedResponse<UserPostDto>> Handle(GetPostsByUserQuery request, CancellationToken cancellationToken)
		{
			int userId = _currentUserService.GetUserId();

			return await _context.Posts
				.Where(x => x.UserId == userId)
				.ProjectTo<UserPostDto>(_mapper.ConfigurationProvider)
				.PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
		}
	}
}
