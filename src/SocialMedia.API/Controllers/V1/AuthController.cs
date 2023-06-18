using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.DTOs;

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


	}
}
