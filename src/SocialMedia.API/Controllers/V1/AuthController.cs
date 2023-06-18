using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Auth.Queries.Profile;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;

namespace SocialMedia.API.Controllers.V1
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ApiControllerBase
	{
		[HttpPost("Login")]
		[ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<TokenDto>> Login(AuthenticateCommand command)
		{
			return await Mediator.Send(command);
		}

		[Authorize]
		[HttpPost("Profile")]
		[ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<ApiResponse<UserDto>>> Profile()
		{
			return await Mediator.Send(new ProfileQuery());
		}


	}
}
