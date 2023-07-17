using Microsoft.AspNetCore.ResponseCompression;
using SocialMedia.API;
using SocialMedia.API.Extensions;
using SocialMedia.Application;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Persistence.Context;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

//Layers

builder.Services
	.AddApplicationServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddWebAPIServices();

builder.Services
	.AddResponseCompression(o => o.EnableForHttps = true)
	.Configure<BrotliCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	// Initialise and seed database
	using var scope = app.Services.CreateScope();
	var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
	await dbContextInitialiser.InitialiseAsync();
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MiddlewareExtensions();

app.Run();
