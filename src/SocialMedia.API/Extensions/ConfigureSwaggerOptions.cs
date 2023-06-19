using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SocialMedia.API.Utils;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SocialMedia.API.Extensions
{
	public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public ConfigureSwaggerOptions(
			IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}

		public void Configure(SwaggerGenOptions options)
		{
			// add swagger document for every API version discovered
			foreach (var description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
				options.EnableAnnotations();
			}
		}

		public void Configure(string? name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
		{
			var info = new OpenApiInfo()
			{
				Title = "Social Media API",
				Contact = new OpenApiContact
				{
					Name = "Jeyson Almonte",
					Email = "jeysom28@gmail.com"
				},
				Description = $"Environment: {SystemUtil.GetEnvironment()}",
				Version = description.ApiVersion.ToString()
			};

			if (description.IsDeprecated)
			{
				info.Description += "This API version has been deprecated.";
			}

			return info;
		}
	}
}