using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Wrappers;

namespace SocialMedia.Application.Common.Mappings
{
	public static class MappingExtensions
	{
		public static Task<ApiPaginatedResponse<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize,
			CancellationToken cancellationToken = default) where TDestination : class
			=> ApiPaginatedResponse<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);

		public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
			=> queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
	}
}
