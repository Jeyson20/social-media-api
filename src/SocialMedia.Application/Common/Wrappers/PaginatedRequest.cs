namespace SocialMedia.Application.Common.Wrappers
{
	public record PaginatedRequest 
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 50;
	}
}
