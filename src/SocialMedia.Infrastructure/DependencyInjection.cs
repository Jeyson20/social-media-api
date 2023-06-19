using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Application.Common.Utils;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Infrastructure.Persistence.Context;
using SocialMedia.Infrastructure.Persistence.Interceptors;
using SocialMedia.Infrastructure.Services;
using System.Text;

namespace SocialMedia.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<AuditableEntitySaveChangesInterceptor>();

			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
				builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
			services.AddScoped<ApplicationDbContextInitialiser>();

			services.AddTransient<IDateTime, DateTimeService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ICurrentUserService, CurrentUserService>();


			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore
			};


			var jwtSettings = configuration.GetSection("JwtConfig");
			services.Configure<JwtConfig>(jwtSettings);
			var appSettings = jwtSettings.Get<JwtConfig>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = appSettings?.Issuer,
					ValidAudience = appSettings?.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings?.Secret ?? ""))
				};
				options.Events = new JwtBearerEvents
				{
					OnChallenge = context =>
					{
						context.HandleResponse();
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						context.Response.ContentType = "application/json";
						context.ErrorDescription = "Provide a valid JWT access token";

						if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
						{
							var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
							context.Response.Headers.Add("x-token-expired", "true");
							context.ErrorDescription = $"The token expired on {authenticationException?.Expires.ToString("yyyy-MM-ddThh:mm:ss")}";
						}
						var result = new ApiResponse<string>(context.ErrorDescription);
						return context.Response.WriteAsync(JsonConvert.SerializeObject(result, settings));
					},
				};
			});

			return services;
		}
	}
}