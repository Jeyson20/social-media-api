﻿using MediatR;
using SocialMedia.Application.Common.Mappings;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand : IRequest<ApiResponse<int>>, IMapFrom<Post>
	{
		public string Description { get; set; } = null!;
		public string? Image { get; set; }
	}
}

