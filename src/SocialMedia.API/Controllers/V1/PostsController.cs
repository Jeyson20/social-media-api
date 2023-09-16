using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Posts.Commands.AddComment;
using SocialMedia.Application.Posts.Commands.CreatePost;
using SocialMedia.Application.Posts.Commands.DeletePost;
using SocialMedia.Application.Posts.DTOs;
using SocialMedia.Application.Posts.Queries.GetAllPosts;
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
		[SwaggerOperation(Summary = "Private: Create post")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiResponse<int>>> CreatePost(CreatePostCommand command)
			=> await Mediator.Send(command);


		[HttpGet]
		[SwaggerOperation(Summary = "Public: Get all posts")]
		[ProducesResponseType(typeof(ApiPaginatedResponse<PostDto>), StatusCodes.Status200OK)]
		public async Task<ActionResult<ApiPaginatedResponse<PostDto>>> GetAllPosts([FromQuery] GetAllPostsQuery query)
			=> await Mediator.Send(query);


		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Public: Get post by id")]
		[ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<PostDto>>> GetPostById(int id)
			 => await Mediator.Send(new GetPostByIdQuery(id));


		[Authorize]
		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Private: Delete Post")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> DeletePost(int id)
			=> await Mediator.Send(new DeletePostCommand(id));

		[Authorize]
		[HttpPost("{postId}/Comments")]
		[SwaggerOperation(Summary = "Private: Add comment to post")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> AddCommentToPost([FromRoute] int postId, AddCommentToPostCommand command)
		{
			if (postId != command.PostId) throw new ApiException("Ids must be equals");

			return await Mediator.Send(command);
		}
	}
}
