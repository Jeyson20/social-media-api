using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.CreateUser;
using SocialMedia.Application.Users.Commands.DeleteUser;
using SocialMedia.Application.Users.Commands.UpdateUser;

namespace SocialMedia.API.Controllers.V1
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ApiControllerBase
	{

		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiResponse<int>>> CreateUser(CreateUserCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ApiResponse<int>>> UpdateUser(int id, UpdateUserCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<int>>> DeleteUser(int id)
		{
			return await Mediator.Send(new DeleteUserCommand(id));
		}
	}
}
