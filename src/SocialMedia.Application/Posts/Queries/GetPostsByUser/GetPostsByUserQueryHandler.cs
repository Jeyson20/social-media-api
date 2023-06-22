using Ardalis.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetPostsByUser
{
	internal class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQuery, ApiPaginatedResponse<PostUserDto>>
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

		public async Task<ApiPaginatedResponse<PostUserDto>> Handle(GetPostsByUserQuery request, CancellationToken cancellationToken)
		{
			int userId = _currentUserService.GetUserId();

			var user = await _context.Posts
				.Where(x => x.UserId == userId).ToListAsync();
			return await _context.Posts
				.Where(x => x.UserId == userId)
				.ProjectTo<PostUserDto>(_mapper.ConfigurationProvider)
				.PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
		}
	}
}
