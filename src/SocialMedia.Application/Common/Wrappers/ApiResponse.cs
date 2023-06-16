namespace SocialMedia.Application.Common.Wrappers
{
	public class ApiResponse<T>
	{
		public ApiResponse(T data, string? message = "OK")
		{
			Succeeded = true;
			Message = message;
			Data = data;
		}

		public ApiResponse(string message)
		{
			Message = message;
		}

		public ApiResponse(bool succeeded, string message, Dictionary<string, string[]> errors)
		{
			Succeeded = succeeded;
			Message = message;
			Errors = errors;
		}

		public bool Succeeded { get; init; } = false;
		public string? Message { get; }
		public T? Data { get; }
		public Dictionary<string, string[]>? Errors { get; init; }
	}
}
