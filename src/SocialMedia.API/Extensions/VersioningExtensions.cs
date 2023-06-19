using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SocialMedia.API.Extensions
{
	public static class VersioningExtensions
	{
		public static void AddVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 0);
				config.AssumeDefaultVersionWhenUnspecified = true;
				config.ReportApiVersions = true;
				config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
																   new HeaderApiVersionReader("x-api-version"),
																   new MediaTypeApiVersionReader("x-api-version"));
			});

			services.AddVersionedApiExplorer(setup =>
			{
				setup.GroupNameFormat = "'v'VVV";
				setup.SubstituteApiVersionInUrl = true;
			});
		}
	}
}
