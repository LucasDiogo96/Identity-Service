using FluentValidation;
using MongoDB.Bson;
using Sample.Identity.App.Transfers;

namespace Sample.Identity.App.Validators
{
    public class IdentityRefreshValidator : AbstractValidator<IdentityRefreshTransfer>
    {
        public IdentityRefreshValidator()
        {
            RuleFor(property => property.AccessToken).NotEmpty();

            RuleFor(property => property.UserId).NotEmpty()
                .Must(e => ObjectId.TryParse(e, out ObjectId objectId));

            RuleFor(property => property.RefreshToken).NotEmpty()
                .Must(e => Guid.TryParse(e, out Guid guid));
        }
    }
}