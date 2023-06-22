using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common.Interfaces;
using System.Security.Claims;

namespace SocialMedia.Infrastructure.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public int GetUserId()
		{
			var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (int.TryParse(userIdString, out int userId))
			{
				return userId;
			}

			return 0;
		}
		public string? Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");

	}
}
