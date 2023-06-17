using Ardalis.Specification;
using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Queries.GetUsers
{
	public record GetUsersQuery : IRequest<ApiPaginatedResponse<UserDto>>
	{
		public Status Status { get; set; }
		public string? Username { get; init; }
		public string? Email { get; set; }
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 10;
	};

	public class UserSpecification : Specification<User>
	{
		public UserSpecification(Status status, string? username, string? email)
		{
			Query.OrderBy(x => x.Id);

			if (status != 0)
				Query.Where(x => x.Status == status);

			if (!string.IsNullOrEmpty(username))
				Query.Search(x => x.Username!, "%" + username + "%");

			if (!string.IsNullOrEmpty(email))
				Query.Search(x => x.Email!, "%" + email + "%");
		}
	}
}
