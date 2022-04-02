using Sample.Identity.Domain.Entities;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.Infra.Contracts
{
    public interface IIdentityProvider
    {
        public UserIdentity SignIn(User user);
    }
}