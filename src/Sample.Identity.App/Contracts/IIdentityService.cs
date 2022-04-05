using Sample.Identity.App.Transfers;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.App.Contracts
{
    public interface IIdentityService
    {
        public UserIdentity SignIn(IdentitySignInTransfer model);

        public UserIdentity Refresh(IdentityRefreshTransfer model);

        public Task VerifyIdentity(string userId, NotificationType type);

        public Task<bool> VerifyEmail(string userid, string verification);

        public Task<bool> VerifyPhone(string userid, string verification);
    }
}