using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Infrastructure.Persistence.Context;
using SocialMedia.Infrastructure.Persistence.Interceptors;
using SocialMedia.Infrastructure.Services;

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

			return services;
		}
	}
}