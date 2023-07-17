using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.DeleteUser;
using SocialMedia.Application.Users.Commands.UpdateUser;
using SocialMedia.Application.Users.DTOs;
using SocialMedia.Application.Users.Queries.GetAllUsers;
using SocialMedia.Application.Users.Queries.GetPostsByUser;
using SocialMedia.Application.Users.Queries.GetUserById;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMedia.API.Controllers.V1
{
	[ApiVersion(ApiVersions.v1)]
	[Route(Routes.ControllerRoute)]
	public class UsersController : ApiControllerBase
	{
		[HttpGet]
		[SwaggerOperation(Summary = "Public: Get all users")]
		[ProducesResponseType(typeof(ApiPaginatedResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiPaginatedResponse<UserDto>>> GetUsers([FromQuery] GetAllUsersQuery query)
			=> await Mediator.Send(query);


		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Public: Get user by id")]
		[ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(int id)
			=> await Mediator.Send(new GetUserByIdQuery(id));


		[Authorize]
		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Private: Update user")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> UpdateUser(int id, UpdateUserCommand command)
		{
			if (id != command.Id) throw new ApiException("Ids must be equals");

			return await Mediator.Send(command);
		}

		[Authorize]
		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Private: Deactivate user")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> DeleteUser(int id)
			=> await Mediator.Send(new DeleteUserCommand(id));


		[Authorize]
		[HttpGet("Posts/Me")]
		[SwaggerOperation(Summary = "Private: Get posts by authenticated user")]
		[ProducesResponseType(typeof(ApiPaginatedResponse<UserPostDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<ApiPaginatedResponse<UserPostDto>>> GetPostsByUser([FromQuery] GetPostsByUserQuery query)
			=> await Mediator.Send(query);

	}
}
