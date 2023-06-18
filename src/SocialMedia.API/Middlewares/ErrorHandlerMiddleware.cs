using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Wrappers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialMedia.API.Middlewares
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				await ValidationResponse(context, ex);
			}
			catch (ApiException ex)
			{
				await Response(context, StatusCodes.Status400BadRequest, ex.Message);
			}
			catch (KeyNotFoundException ex)
			{
				await Response(context, StatusCodes.Status404NotFound, ex.Message);
			}
			catch (Exception ex)
			{
				await Response(context, StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


		private static async Task Response(HttpContext context, int statusCode, string message)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			var responseModel = new ApiResponse<string>(message);

			await context.Response.WriteAsJsonAsync(responseModel, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
			});
		}

		private static async Task ValidationResponse(HttpContext context, ValidationException e)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

			var responseModel = new ApiResponse<string>(
				succeeded: false,
				message: e.Message,
				errors: e.Errors
				.ToDictionary(entry => entry.Key, entry => entry.Value.ToArray()));

			await context.Response.WriteAsJsonAsync(responseModel, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
			});
		}
	}
}
