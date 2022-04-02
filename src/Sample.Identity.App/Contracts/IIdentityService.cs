using Sample.Identity.App.Transfers;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.App.Contracts
{
    public interface IIdentityService
    {
        public Task<UserIdentity?> SignIn(IdentitySignInTransfer model);

        public Task<UserIdentity?> Refresh(IdentityRefreshTransfer model);
    }
}