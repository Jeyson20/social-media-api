using SocialMedia.API.Middlewares;

namespace SocialMedia.API.Extensions
{
	public static class AppExtensions
	{
		public static void MiddlewareExtensions(this IApplicationBuilder app)
		{
			app.UseMiddleware<ErrorHandlerMiddleware>();
		}
	}
}
