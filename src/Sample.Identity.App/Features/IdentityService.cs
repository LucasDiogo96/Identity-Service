using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Domain.Events;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.App.Features
{
    public class IdentityService : IIdentityService
    {
        private readonly AppSettings settings;
        private readonly INotification notification;
        private readonly IUnitOfWork unitOfWork;
        private readonly IIdentityProvider provider;
        private readonly ILogger<IdentityService> logger;
        private readonly IUserDomainService userDomainService;
        private readonly ICacheManager cacheManager;
        private readonly INotificationService notificationService;
        private readonly IPublishEndpoint publisher;

        public IdentityService(
            IOptions<AppSettings> settings,
            IIdentityProvider provider,
            IUnitOfWork unitOfWork,
            ILogger<IdentityService> logger,
            INotification notification,
            IUserDomainService userDomainService,
            ICacheManager cacheManager,
            INotificationService notificationService,
            IPublishEndpoint publisher)
        {
            this.logger = logger;
            this.publisher = publisher;
            this.cacheManager = cacheManager;
            this.provider = provider;
            this.settings = settings.Value;
            this.notification = notification;
            this.unitOfWork = unitOfWork;
            this.userDomainService = userDomainService;
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>jwt token</returns>
        public UserIdentity SignIn(IdentitySignInTransfer model)
        {
            // Find user by username
            User user = unitOfWork.UserRepository.Get(e => e.UserName == model.UserName).FirstOrDefault();

            // Validate user signIn business rules in domain layer
            if (!userDomainService.ValidateUserSignIn(user, model.Password))
            {
                OnSignInFail(user);

                return default;
            }

            // Create JWT token for the current user
            UserIdentity identity = provider.SignIn(user);

            OnSignInSucceed(user, identity, model);

            return identity;
        }

        /// <summary>
        /// Refresh jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserIdentity Refresh(IdentityRefreshTransfer model)
        {
            // Get identity stored on cache
            UserIdentity identity = cacheManager.Get<UserIdentity>(model.RefreshToken);

            // Check that the token has not expired in the cache
            if (identity is null ||
               !identity.ValidateRefreshToken(model.AccessToken, model.RefreshToken, model.UserId))
            {
                notification.AddNotification(MappedErrorsEnum.RefreshTokenExpired);

                return default;
            }

            OnRefreshSucceed(model, identity);

            return identity;
        }

        public async Task VerifyIdentity(string userId, NotificationType type)
        {
            // Retrieve user data to send a random code to be confirmed, this code
            // will be store in cache to be temporary
            User user = await unitOfWork.UserRepository.GetById(userId);

            string code = new Random().Next(0, 1000000).ToString("D6");

            cacheManager.Add($"identity-verification-{user.Id}-{type}".ToLower(), code, TimeSpan.FromMinutes(settings.IdentityConfirmTimespan));

            // send notification only if identity isn't confirmed
            if (type is NotificationType.SMS && !user.PhoneNumberConfirmed)
            {
                await Task.Factory.StartNew(() => notificationService.SendIdentityConfirmSms(code, user.PhoneNumber));
            }
            else if (type is NotificationType.EMAIL && !user.EmailConfirmed)
            {
                await Task.Factory.StartNew(() => notificationService.SendIdentityConfirmEmail(code, user.Email));
            }
        }

        /// <summary>
        /// Verify identity email
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="verification"></param>
        /// <returns>bool</returns>
        public async Task<bool> VerifyEmail(string userid, string verification)
        {
            // Create key to store the verification code
            string key = $"identity-verification-{userid}-{NotificationType.EMAIL}"
                         .ToLower();

            User user = await unitOfWork.UserRepository.GetById(userid);

            user.ConfirmEmail();

            return VerifyCode(user, key, verification);
        }

        /// <summary>
        /// Verify identity phone
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="verification"></param>
        /// <returns></returns>
        public async Task<bool> VerifyPhone(string userid, string verification)
        {
            string key = $"identity-verification-{userid}-{NotificationType.SMS}"
                        .ToLower();

            User user = await unitOfWork.UserRepository.GetById(userid);

            user.ConfirmPhone();

            return VerifyCode(user, key, verification);
        }

        private bool VerifyCode(User user, string key, string verification)
        {
            string code = cacheManager.Get<string>(key);

            // If verification fails, the entity will not be updated
            if (!verification.Equals(code))
            {
                return false;
            }

            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();

            return true;
        }

        /// <summary>
        /// Behavior to be performed when an authentication request is not valid
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="user"></param>
        private void OnSignInFail(User user)
        {
            if (user == null || !settings.UserLockoutEnabledByDefault ||
               !notification.Exists(MappedErrorsEnum.UsernameOrPasswordIncorrect))
            {
                return;
            }

            // Update failed login count
            userDomainService.UpdateUserSignInOnFail(user,
                settings.AccountLockoutTimeSpan, settings.MaxFailedAttempts);

            // Updated stored data
            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();
        }

        /// <summary>
        /// Behavior to be performed when an authentication request is valid
        /// </summary>
        /// <param name="user"></param>
        private void OnSignInSucceed(User user, UserIdentity identity, IdentitySignInTransfer model)
        {
            // Updates user faults only if it is greater than 0 as
            // there is no need to access the database always
            if (user.AccessFailedCount is not 0)
            {
                user.ResetSignInAttempts();

                unitOfWork.UserRepository.Update(user);

                unitOfWork.Save();
            }

            // Persist identity in cache for refresh token
            cacheManager.Add(identity.RefreshToken, identity, TimeSpan.FromMinutes(settings.RefreshExpirationTime));

            // Build user sign event
            UserSignInEvent @event = new(
                identity.UserId,
                identity.Username,
                identity.CreateDate,
                identity.ExpiryDate,
                model.RemoteAddress,
                model.Coordinates);

            // Publish
            publisher.Publish(@event).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Behavior to be performed when a refresh token request is valid
        /// </summary>
        /// <param name="model"></param>
        /// <param name="identity"></param>
        /// <returns>Task</returns>
        private void OnRefreshSucceed(IdentityRefreshTransfer model, UserIdentity identity)
        {
            // Delete the previous authentication.
            cacheManager.Remove(model.RefreshToken);

            User user = unitOfWork.UserRepository.GetById(model.UserId).GetAwaiter().GetResult();

            // Authenticate token
            identity = provider.SignIn(user);

            // Store refresh token in redis
            cacheManager.Add(identity.RefreshToken, identity, TimeSpan.FromMinutes(settings.RefreshExpirationTime));

            logger.LogInformation($"The user {identity.Username} has been authenticated..");
        }
    }
}