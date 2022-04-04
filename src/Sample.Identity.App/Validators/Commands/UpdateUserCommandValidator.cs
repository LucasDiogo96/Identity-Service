using FluentValidation;
using Sample.Identity.Domain.Commands;

namespace Sample.Identity.App.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(property => property.Id).NotEmpty();

            RuleFor(property => property.Username).NotEmpty()
                .MinimumLength(4);

            RuleFor(property => property.FirstName).NotEmpty()
                .MinimumLength(2);

            RuleFor(property => property.LastName).NotEmpty();

            RuleFor(property => property.PhoneNumber).NotEmpty();

            RuleFor(property => property.CultureCode).NotEmpty()
                    .Matches(@"^[a-z]{2}-[A-Z]{2}$");
        }
    }
}