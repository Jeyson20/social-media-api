﻿namespace SocialMedia.API.Utils
{
	public static class ApiVersions
	{
		public const string v1 = "1.0";
		public const string v2 = "2.0";
	}

	public static class Routes
	{
		public const string GlobalPrefix = "api/v{version:apiVersion}";
		public const string ControllerRoute = "[controller]";
	}
}
