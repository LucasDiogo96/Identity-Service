using Moq;
using NUnit.Framework;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Services;
using System;

namespace Sample.Identity.Tests.Services
{
    [TestFixture]
    public class UserDomainServiceTests
    {
        private MockRepository mockRepository;

        private Mock<INotification> mockNotification;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNotification = this.mockRepository.Create<INotification>();
        }

        private UserDomainService CreateService()
        {
            return new UserDomainService(
                this.mockNotification.Object);
        }

        [Test]
        public void ValidateUserSignIn_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            UserDomainService? service = this.CreateService();
            User user = null;
            string password = null;

            // Act
            bool result = service.ValidateUserSignIn(
                user,
                password);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void UpdateUserSignInOnFail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            UserDomainService? service = this.CreateService();
            User user = null;
            int accountLockoutTimeSpan = 0;
            int maxFailedAttempts = 0;

            // Act
            service.UpdateUserSignInOnFail(
                user,
                accountLockoutTimeSpan,
                maxFailedAttempts);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void UpdateUserRecoveredPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            UserDomainService? service = this.CreateService();
            User user = null;
            string recoveryId = null;
            string password = null;

            // Act
            bool result = service.UpdateUserRecoveredPassword(
                user,
                recoveryId,
                password);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}