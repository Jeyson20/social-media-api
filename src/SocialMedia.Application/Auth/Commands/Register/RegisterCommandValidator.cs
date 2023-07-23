using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;

namespace SocialMedia.Application.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IApplicationDbContext _context;
        public RegisterCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("{PropertyName} isn't valid")
            .MustAsync(BeUniqueEmail!).WithMessage("The specified email already exists.");

            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(BeUniqueUsername!).WithMessage("The specified username already exists.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MinimumLength(8).WithMessage("{PropertyName} must have a minimum of {MinLength} characters");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(l => l.Username != username, cancellationToken);
        }
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(l => l.Email != email, cancellationToken);
        }

    }
}
