using FluentValidation;
using Sample.Identity.App.Transfers.Recovery;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Validators
{
    public class PasswordRecoveryValidator : AbstractValidator<PasswordRecoveryTransfer>
    {
        public PasswordRecoveryValidator()
        {
            RuleFor(property => property.UserName).NotEmpty()
               .MinimumLength(4);

            RuleFor(property => property.Password).NotEmpty()
                .Must(e => Password.ValidatePasswordPattern(e));

            RuleFor(property => property.PasswordConfirm).NotEmpty()
                .Equal(e => e.Password);

            RuleFor(property => property.RecoveryId).NotEmpty()
                .Must(e => Guid.TryParse(e, out Guid guid));
        }
    }
}