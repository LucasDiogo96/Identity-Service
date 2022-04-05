using FluentValidation;
using Sample.Identity.App.Transfers.Recovery;

namespace Sample.Identity.App.Validators
{
    public class PasswordRecoveryRequestValidator : AbstractValidator<PasswordRecoveryRequestTransfer>
    {
        public PasswordRecoveryRequestValidator()
        {
            RuleFor(property => property.UserName).NotEmpty()
               .MinimumLength(4);

            RuleFor(property => property.NotificationType).NotEmpty()
                .IsInEnum();
        }
    }
}