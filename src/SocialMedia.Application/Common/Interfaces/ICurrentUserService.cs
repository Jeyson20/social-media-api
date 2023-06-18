namespace SocialMedia.Application.Common.Interfaces
{
	public interface ICurrentUserService
	{
		int? GetUserId();
		string? Username { get; }
	}
}
