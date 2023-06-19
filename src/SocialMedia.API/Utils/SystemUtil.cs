namespace SocialMedia.API.Utils
{
	public static class SystemUtil
	{
		public static string? GetEnvironment()
			=> Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		public static bool IsProduction()
			=> GetEnvironment()?.ToUpper() == "PRODUCTION";
	}
}
