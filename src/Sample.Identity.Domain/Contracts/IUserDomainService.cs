using Sample.Identity.Domain.Entities;

namespace Sample.Identity.Domain.Contracts
{
    public interface IUserDomainService
    {
        public bool UpdateUserRecoveredPassword(User user, string recoveryId, string password);

        public bool ValidateUserSignIn(User user, string password);

        public void UpdateUserSignInOnFail(User user, int defaultAccountLockoutTimeSpan, int maxFailedAccessAttemptsBeforeLockout);
    }
}