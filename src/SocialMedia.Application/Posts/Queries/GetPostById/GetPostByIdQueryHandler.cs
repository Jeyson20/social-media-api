using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.DTOs;
using SocialMedia.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Posts.Queries.GetPostById
{
	internal class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, ApiResponse<PostWithCommentsAndLikesDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public GetPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
		{
			_context = context;
			_mapper = mapper;
			_currentUserService = currentUserService;
		}

		public async Task<ApiResponse<PostWithCommentsAndLikesDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
		{
			int userId = _currentUserService.GetUserId();

			var post = await _context.Posts
				.Include(x => x.Comments)
				.Include(x => x.Likes)
				.ProjectTo<PostWithCommentsAndLikesDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(x => x.Id == request.Id);

			if (post is null)
			{
				throw new KeyNotFoundException(nameof(post));
			}

			return new ApiResponse<PostWithCommentsAndLikesDto>(post);
		}
	}
}
