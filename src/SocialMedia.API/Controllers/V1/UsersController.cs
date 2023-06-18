using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.CreateUser;
using SocialMedia.Application.Users.Commands.DeleteUser;
using SocialMedia.Application.Users.Commands.UpdateUser;
using SocialMedia.Application.Users.DTOs;
using SocialMedia.Application.Users.Queries.GetUserById;
using SocialMedia.Application.Users.Queries.GetUsers;

namespace SocialMedia.API.Controllers.V1
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ApiControllerBase
	{
		[HttpGet]
		[ProducesResponseType(typeof(ApiPaginatedResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiPaginatedResponse<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
		{
			return await Mediator.Send(query);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(int id)
		{
			return await Mediator.Send(new GetUserByIdQuery(id));
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		public async Task<ActionResult<ApiResponse<int>>> CreateUser(CreateUserCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
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
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> DeleteUser(int id)
		{
			return await Mediator.Send(new DeleteUserCommand(id));
		}
	}
}
