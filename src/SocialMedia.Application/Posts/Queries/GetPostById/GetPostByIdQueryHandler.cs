using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;

namespace SocialMedia.Application.Posts.Queries.GetPostById
{
	internal class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, ApiResponse<PostDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		public GetPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ApiResponse<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
		{
			var post = await _context.Posts
				.Where(x => x.Id == request.Id)
				.Include(x => x.Comments)
				.Include(x => x.Likes)
				.ProjectTo<PostDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Post not found");

			return new ApiResponse<PostDto>(post);
		}
	}
}
