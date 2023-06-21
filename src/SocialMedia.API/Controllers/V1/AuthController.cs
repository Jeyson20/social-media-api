using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Utils;
using SocialMedia.Application.Auth.Commands.Authenticate;
using SocialMedia.Application.Auth.Commands.RefreshToken;
using SocialMedia.Application.Auth.DTOs;
using SocialMedia.Application.Auth.Queries.Profile;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.Commands.CreateUser;
using SocialMedia.Application.Users.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMedia.API.Controllers.V1
{
	[ApiVersion(ApiVersions.v1)]
	[Route(Routes.ControllerRoute)]
	public class AuthController : ApiControllerBase
	{
		[HttpPost("Login")]
		[SwaggerOperation(Summary = "Authenticate user")]
		[ProducesResponseType(typeof(AuthDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<AuthDto>> Login(AuthenticateCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPost("RefreshToken")]
		[SwaggerOperation(Summary = "Generate new token by refrehToken")]
		[ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<TokenDto>> RefreshToken(RefreshTokenCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPost("Signup")]
		[SwaggerOperation(Summary = "Create user")]
		[ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		public async Task<ActionResult<ApiResponse<int>>> Signup(CreateUserCommand command)
		{
			return await Mediator.Send(command);
		}

		[Authorize]
		[HttpPost("Profile")]
		[SwaggerOperation(Summary = "Get authenticated user profile")]
		[ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<ApiResponse<UserDto>>> Profile()
		{
			return await Mediator.Send(new ProfileQuery());
		}


	}
}
