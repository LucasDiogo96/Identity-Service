using FluentValidation;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(property => property.Email).NotEmpty()
                .EmailAddress();

            RuleFor(property => property.Password).NotEmpty()
                .Must(e => Password.ValidatePasswordPattern(e));

            RuleFor(property => property.Username).NotEmpty()
                .MinimumLength(4);

            RuleFor(property => property.FirstName).NotEmpty()
                .MinimumLength(2);

            RuleFor(property => property.LastName).NotEmpty();

            RuleFor(property => property.PhoneNumber).NotEmpty();

            RuleFor(property => property.CultureCode).NotEmpty()
                    .Matches(@"/^[a-z]{2,3}(?:-[A-Z]{2,3}(?:-[a-zA-Z]{4})?)?$/");
        }
    }
}