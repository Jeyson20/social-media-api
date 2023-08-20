namespace SocialMedia.Application.Common.Wrappers
{
	public class ApiResponse<T>
	{
		private ApiResponse(T data, string? message)
		{
			Succeeded = true;
			Message = message;
			Data = data;
		}

		private ApiResponse(string message)
		{
			Message = message;
		}

		private ApiResponse(bool succeeded, string message, Dictionary<string, string[]> errors)
		{
			Succeeded = succeeded;
			Message = message;
			Errors = errors;
		}

		public static ApiResponse<T> Success(T data, string? message = "OK") => new(data, message);
		public static ApiResponse<T> Error(string message = "Error") => new(message);
		public static ApiResponse<T> ValidationErrors(bool succeeded, string message, Dictionary<string, string[]> errors) 
			=> new(succeeded, message,errors);

		public bool Succeeded { get; init; } = false;
		public string? Message { get; }
		public T? Data { get; }
		public Dictionary<string, string[]>? Errors { get; init; }
	}
}
