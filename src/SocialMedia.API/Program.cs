using SocialMedia.API.Extensions;
using SocialMedia.Application;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Persistence.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Layers
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();


builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(json =>
{
	json.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	json.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MiddlewareExtensions();

app.Run();
