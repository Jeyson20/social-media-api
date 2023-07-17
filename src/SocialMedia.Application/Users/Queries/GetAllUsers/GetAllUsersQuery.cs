using Ardalis.Specification;
using MediatR;
using SocialMedia.Application.Common.Wrappers;
using SocialMedia.Application.Users.DTOs;
using SocialMedia.Domain.Entities.Users;
using SocialMedia.Domain.Enums;

namespace SocialMedia.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery (
		Status Status,
		string? Username,
		string? Email) : PaginatedRequest, IRequest<ApiPaginatedResponse<UserDto>>;


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
