using FluentValidation;
using Sample.Identity.App.Transfers.Recovery;

namespace Sample.Identity.App.Validators
{
    public class PasswordRecoveryConfirmValidator : AbstractValidator<PasswordRecoveryConfirmTransfer>
    {
        public PasswordRecoveryConfirmValidator()
        {
            RuleFor(property => property.UserName).NotEmpty()
                .MinimumLength(4);

            RuleFor(property => property.ConfirmationCode).NotEmpty()
                .Length(4);
        }
    }
}