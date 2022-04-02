using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Enumerators;
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

        public IdentityService(
            IOptions<AppSettings> settings,
            IIdentityProvider provider,
            IUnitOfWork unitOfWork,
            ILogger<IdentityService> logger,
            INotification notification,
            IUserDomainService userDomainService,
            ICacheManager cacheManager)
        {
            this.logger = logger;
            this.cacheManager = cacheManager;
            this.provider = provider;
            this.settings = settings.Value;
            this.notification = notification;
            this.unitOfWork = unitOfWork;
            this.userDomainService = userDomainService;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserIdentity?> SignIn(IdentitySignInTransfer model)
        {
            // Find user
            User? user = unitOfWork.UserRepository.Get(e =>
            e.Username == model.Username && model.Tenant == model.Tenant)?.First();

            // Validate user signIn inside domain layer
            if (!userDomainService.ValidateUserSignIn(user, model.Password))
            {
                OnSignInFail(model, user);

                return default;
            }

            // Authenticate token
            UserIdentity identity = provider.SignIn(user);

            OnSignInSucceed(user, identity);

            await cacheManager.Add(identity.RefreshToken, identity, TimeSpan.FromMinutes(settings.RefreshExpirationTime));

            return identity;
        }

        /// <summary>
        /// Refresh jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserIdentity?> Refresh(IdentityRefreshTransfer model)
        {
            // Get identity stored on cache
            UserIdentity identity = await cacheManager.Get<UserIdentity>(model.RefreshToken);

            // Check that the token has not expired in the cache
            if (identity is null ||
               !identity.ValidateRefreshToken(model.AccessToken, model.RefreshToken, model.UserId))
            {
                notification.AddNotification(MappedErrorsEnum.RefreshTokenExpired);

                return default;
            }

            await OnRefreshSucceed(model, identity);

            // Handle Authethentication
            return identity;
        }

        /// <summary>
        /// Behavior to be performed when an authentication request is not valid
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="user"></param>
        private void OnSignInFail(IdentitySignInTransfer auth, User user)
        {
            userDomainService.UpdateUserSignInOnFail(user, settings.AccountLockoutTimeSpan, settings.MaxFailedAttempts);

            logger.LogInformation($"Failed attempts: {user.AccessFailedCount}/{settings.MaxFailedAttempts}. | User: {auth.Username}");

            unitOfWork.UserRepository.Update(user);

            logger.LogInformation($"The authentication was not allowed. | User: {auth.Username}", notification.GetNotifications());
        }

        /// <summary>
        /// Behavior to be performed when an authentication request is valid
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        private void OnSignInSucceed(User user, UserIdentity identity)
        {
            if (user.AccessFailedCount is 0)
            {
                return;
            }

            user.ResetSignInAttempts();

            unitOfWork.UserRepository.Update(user);

            logger.LogInformation($"Authenticating.. | User: {user.Username}.");

            // Save changes
            unitOfWork.Save();
        }

        /// <summary>
        /// Behavior to be performed when a refresh token request is valid
        /// </summary>
        /// <param name="model"></param>
        /// <param name="identity"></param>
        /// <returns>Task</returns>
        private async Task OnRefreshSucceed(IdentityRefreshTransfer model, UserIdentity identity)
        {
            // Delete the previous authentication.
            await cacheManager.Remove(model.RefreshToken);

            User user = unitOfWork.UserRepository.GetById(model.UserId);

            // Authenticate token
            identity = provider.SignIn(user);

            // Store refresh token in redis
            await cacheManager.Add(identity.RefreshToken, identity, TimeSpan.FromMinutes(settings.RefreshExpirationTime));

            logger.LogInformation($"The user {identity.Username} has been authenticated..");
        }
    }
}