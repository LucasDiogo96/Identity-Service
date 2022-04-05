using FluentValidation;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(property => property.Id).NotEmpty();

            RuleFor(property => property.Username)
                .MinimumLength(4)
                .When(username => username is not null);

            RuleFor(property => property.FirstName)
                .NotEmpty()
                .When(firstname => firstname is not null);

            RuleFor(property => property.LastName)
                .NotEmpty()
                .When(lastname => lastname is not null);

            RuleFor(property => property.PhoneNumber)
                .NotEmpty()
                .When(lastname => lastname is not null);

            RuleFor(property => property.CultureCode)
                 .Matches(@"^[a-z]{2}-[A-Z]{2}$")
                 .When(culture => culture is not null);

            RuleFor(property => property.Password)
                .Matches(e => Password.GetPattern())
                .When(password => password is not null);
        }
    }
}