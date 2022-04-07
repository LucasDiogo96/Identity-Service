using System;
using System.Collections.Generic;
using GenFu;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;
using Sample.Identity.Infra.Providers;

namespace Tradeforce.Identity.Tests
{
    public class IdentityProviderTests
    {
        private readonly AppSettings settings;

        public IdentityProviderTests()
        {
            GenFu.GenFu.Configure<AppSettings>()
              .Fill(p => p.SecretKey, "f*MT6HfrJ!71^iF*A$gUGGU8!T3y9Pporlolayd6IlR9femoSj")
              .Fill(p => p.Issuer, "https://localhost:63526")
              .Fill(p => p.Audience, "Sample.Test")
              .Fill(p => p.TokenExpirationTime, 1440)
              .Fill(p => p.RefreshExpirationTime, 4320)
                .Fill(p => p.RefreshExpirationTime, 4320);

            settings = A.New<AppSettings>();
        }

        [Test]
        public void SignIn_TokenCreation_NotNull()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.NotNull(result);
        }

        [Test]
        public void SignIn_TokenCreation_TokenIsNotEmpty()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.IsNotEmpty(result.AccessToken);
        }

        [Test]
        public void SignIn_TokenCreation_RefreshTokenIsNotEmpty()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.IsNotEmpty(result.RefreshToken);
        }

        [Test]
        public void SignIn_TokenCreation_UserNotNull()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.NotNull(result.UserId);
        }

        [Test]
        public void SignIn_TokenCreation_CreatedDateGreaterThanDefault()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.True(result.CreateDate > DateTime.MinValue);
        }

        [Test]
        public void SignIn_TokenCreation_ExpiryDateGreaterThanDefault()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.True(result.ExpiryDate > DateTime.MinValue);
        }

        [Test]
        public void SignIn_TokenCreation_ExpiryDateGreaterCreateDate()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.True(result.ExpiryDate > result.CreateDate);
        }

        [Test]
        public void SignIn_TokenCreation_UserIdIsNotEmpty()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.AreNotEqual(default, result.UserId);
        }

        [Test]
        public void SignIn_TokenCreation_UserNameIsNotEmpty()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            UserIdentity result = provider.SignIn(A.New<User>());

            Assert.IsNotEmpty(result.Username);
        }

        [Test]
        public void SignIn_TokenCreation_WithUserNull()
        {
            IOptions<AppSettings> options = Options.Create(settings);

            IIdentityProvider provider = new IdentityProvider(options);

            User user = null;

            Assert.Throws<NullReferenceException>(() => provider.SignIn(user));
        }
    }
}