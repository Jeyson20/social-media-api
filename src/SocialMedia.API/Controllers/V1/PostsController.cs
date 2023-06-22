﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.Commands.CreatePost;
using SocialMedia.Application.Posts.DTOs;
using SocialMedia.Application.Posts.Queries.GetPostById;
using SocialMedia.Application.Posts.Queries.GetPostsByUser;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMedia.API.Controllers.V1
{
	[ApiVersion(ApiVersions.v1)]
	[Route(Routes.ControllerRoute)]
	[Authorize]
	public class PostsController : ApiControllerBase
	{
		[HttpPost]
		[SwaggerOperation(Summary = "Create post")]
		public async Task<ActionResult<ApiResponse<int>>> CreatePost(CreatePostCommand command)
		{
			return await Mediator.Send(command);
		}

		
	}
}
