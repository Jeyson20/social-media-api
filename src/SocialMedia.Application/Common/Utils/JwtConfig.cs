namespace SocialMedia.Application.Common.Utils
{
	public class JwtConfig
	{
		public string Secret { get; set; } = null!;
		public string Issuer { get; set; } = null!;
		public string Audience { get; set; } = null!;
	}
}
