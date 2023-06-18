using FluentValidation;

namespace SocialMedia.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
        public UpdateUserCommandValidator()
        {
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("{PropertyName} is required");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("{PropertyName} is required");

			RuleFor(x => x.DateOfBirth)
				.NotEmpty().WithMessage("{PropertyName} is required");


		}
    }
}
