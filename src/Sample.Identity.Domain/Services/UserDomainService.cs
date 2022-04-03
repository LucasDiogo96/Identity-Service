using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly INotification notification;

        public UserDomainService(INotification notification)
        {
            this.notification = notification;
        }

        public bool ValidateUserSignIn(User user, string password)
        {
            // user not exists
            if (user is null)
            {
                notification.AddNotification(MappedErrorsEnum.UsernameOrPasswordIncorrect);

                return false;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(password) || !user.Password.Verify(password))
            {
                notification.AddNotification(MappedErrorsEnum.UsernameOrPasswordIncorrect);

                return false;
            }

            // user temporarily blocked by failed attemptsUser temporary
            if (user.LockoutEndDateUtc.HasValue && user.LockoutEndDateUtc >= DateTime.UtcNow)
            {
                notification.AddNotification(MappedErrorsEnum.UserBlockedForManyFailedAttempts);

                return false;
            }

            if (user.Blocked)
                notification.AddNotification(MappedErrorsEnum.UserBlocked);

            if (!user.Active)
                notification.AddNotification(MappedErrorsEnum.UserInactive);

            return !notification.HasNotifications();
        }

        public void UpdateUserSignInOnFail(User user, int accountLockoutTimeSpan, int maxFailedAttempts)
        {
            // Increment the user access failed account
            user.OnFailedSignInAttempt();

            // If the access failed account is greater than max attempts the user should be blocked
            if (user.AccessFailedCount >= maxFailedAttempts)
            {
                user.LockoutEndDateUtc = DateTime.UtcNow.AddMinutes(accountLockoutTimeSpan);

                notification.AddNotification(MappedErrorsEnum.UserBlockedForManyFailedAttempts);
            }
        }

        public bool UpdateUserRecoveredPassword(User user, string recoveryId, string password)
        {
            RecoveryCode recovery = user.PasswordRecoveries.FirstOrDefault(e => e.Identifier == recoveryId);

            if (recovery is null && !recovery.Identifier.Equals(recovery))
            {
                return false;
            }

            user.PasswordRecoveries.Remove(recovery);

            recovery.Inactivate();

            user.PasswordRecoveries.Add(recovery);

            user.Password = new Password(password);

            return true;
        }
    }
}