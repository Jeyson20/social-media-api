using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Application.Common.Wrappers
{
	public class ApiPaginatedResponse<T>
	{
		public IReadOnlyCollection<T> Data { get; }
		public int PageNumber { get; }
		public int TotalPages { get; }
		public int TotalCount { get; }

		public ApiPaginatedResponse(IReadOnlyCollection<T> data, int count, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			TotalCount = count;
			Data = data;
		}

		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;

		public static async Task<ApiPaginatedResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
			CancellationToken cancellationToken = default)
		{
			var count = await source
				.CountAsync(cancellationToken);

			var items = await source
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return new ApiPaginatedResponse<T>(items, count, pageNumber, pageSize);
		}
	}
}
