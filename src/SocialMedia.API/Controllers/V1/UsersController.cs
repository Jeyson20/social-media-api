using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.CreateUser;

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

	}
}
