using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SocialMedia.API.Extensions;
using SocialMedia.API.Utils;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace SocialMedia.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
		{
			services.AddRouting(x => x.LowercaseUrls = true);

			services.AddControllers(o =>
			{
				o.UseGeneralRoutePrefix(Routes.GlobalPrefix);

			}).AddJsonOptions(json =>
			{
				json.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				json.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});


			services.AddHttpContextAccessor();

			services.AddVersioning();

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			services.AddSwaggerGen(c =>
			{
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Type into the textbox: Bearer {your JWT token}.",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				c.AddSecurityDefinition("Bearer", securityScheme);

				var securityRequirement = new OpenApiSecurityRequirement
					{
						{ securityScheme, new[] { "Bearer" } }
					};

				c.AddSecurityRequirement(securityRequirement);
			});

			return services;
		}
	}
}
