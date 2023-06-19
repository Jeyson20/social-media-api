using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.DeleteUser;
using SocialMedia.Application.Users.Commands.UpdateUser;
using SocialMedia.Application.Users.DTOs;
using SocialMedia.Application.Users.Queries.GetUserById;
using SocialMedia.Application.Users.Queries.GetUsers;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMedia.API.Controllers.V1
{
	[Authorize]
	[ApiVersion(ApiVersions.v1)]
	[Route(Routes.ControllerRoute)]
	public class UsersController : ApiControllerBase
	{
		[HttpGet]
		[SwaggerOperation(Summary = "Get users with pagination")]
		[ProducesResponseType(typeof(ApiPaginatedResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiPaginatedResponse<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
		{
			return await Mediator.Send(query);
		}


		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Get user by id")]
		[ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(int id)
		{
			return await Mediator.Send(new GetUserByIdQuery(id));
		}

		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Update user")]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> UpdateUser(int id, UpdateUserCommand command)
		{
			if (id != command.Id)
			{
				throw new ApiException("Ids must be equals");
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Deactivate user")]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> DeleteUser(int id)
		{
			return await Mediator.Send(new DeleteUserCommand(id));
		}
	}
}
