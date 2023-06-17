using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApiControllerBase : ControllerBase
	{
		private ISender? _mediador;
		protected ISender Mediator
		{
			get
			{
#pragma warning disable CS8603 // Possible null reference return.
				return _mediador ??= HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8603 // Possible null reference return.
			}
		}
	}
}
