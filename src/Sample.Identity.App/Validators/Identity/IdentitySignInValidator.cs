using FluentValidation;
using Sample.Identity.App.Transfers;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Validators
{
    public class IdentitySignInValidator : AbstractValidator<IdentitySignInTransfer>
    {
        public IdentitySignInValidator()
        {
            RuleFor(property => property.UserName).NotEmpty()
                .MinimumLength(4);

            RuleFor(property => property.Password).NotEmpty()
                .Matches(e => Password.GetPattern());
        }
    }
}