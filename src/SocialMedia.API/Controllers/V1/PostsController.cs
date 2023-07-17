using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.Commands.CreatePost;
using SocialMedia.Application.Posts.Commands.DeletePost;
using SocialMedia.Application.Posts.DTOs;
using SocialMedia.Application.Posts.Queries.GetPostById;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMedia.API.Controllers.V1
{
	[ApiVersion(ApiVersions.v1)]
	[Route(Routes.ControllerRoute)]

	public class PostsController : ApiControllerBase
	{
		[Authorize]
		[HttpPost]
		[SwaggerOperation(Summary = "Create post")]
		public async Task<ActionResult<ApiResponse<int>>> CreatePost(CreatePostCommand command)
		{
			return await Mediator.Send(command);
		}


		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Get post by id")]
		public async Task<ActionResult<ApiResponse<PostDto>>>
			GetPost(int id)
		{
			return await Mediator.Send(new GetPostByIdQuery(id));
		}

		[Authorize]
		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Delete Post")]
		public async Task<ActionResult<ApiResponse<int>>>
			DeletePost(int id)
		{
			return await Mediator.Send(new DeletePostCommand(id));
		}

	}
}
