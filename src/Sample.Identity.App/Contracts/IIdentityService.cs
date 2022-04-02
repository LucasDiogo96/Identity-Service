using Sample.Identity.App.Transfers;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.App.Contracts
{
    public interface IIdentityService
    {
        public UserIdentity SignIn(IdentitySignInTransfer model);

        public UserIdentity Refresh(IdentityRefreshTransfer model);
    }
}