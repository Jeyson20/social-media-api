using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetAllPosts
{
	public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ApiPaginatedResponse<PostDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetAllPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiPaginatedResponse<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
		{
			return await _context.Posts
				.Include(x => x.User)
				.ProjectTo<PostDto>(_mapper.ConfigurationProvider)
				.PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
		}
	}
}
