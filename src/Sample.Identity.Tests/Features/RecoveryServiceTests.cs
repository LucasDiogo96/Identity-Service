using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Features;
using Sample.Identity.App.Transfers.Recovery;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;
using System;

namespace Sample.Identity.Tests.Features
{
    [TestFixture]
    public class RecoveryServiceTests
    {
        private MockRepository mockRepository;

        private Mock<INotificationService> mockNotificationService;
        private Mock<IOptions<AppSettings>> mockOptions;
        private Mock<ICacheManager> mockCacheManager;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IUserDomainService> mockUserDomainService;
        private Mock<ILogger<RecoveryService>> mockLogger;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNotificationService = this.mockRepository.Create<INotificationService>();
            this.mockOptions = this.mockRepository.Create<IOptions<AppSettings>>();
            this.mockCacheManager = this.mockRepository.Create<ICacheManager>();
            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
            this.mockUserDomainService = this.mockRepository.Create<IUserDomainService>();
            this.mockLogger = this.mockRepository.Create<ILogger<RecoveryService>>();
        }

        private RecoveryService CreateService()
        {
            return new RecoveryService(
                this.mockNotificationService.Object,
                this.mockOptions.Object,
                this.mockCacheManager.Object,
                this.mockUnitOfWork.Object,
                this.mockUserDomainService.Object,
                this.mockLogger.Object);
        }

        [Test]
        public void SendRecoveryCode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RecoveryService? service = this.CreateService();
            PasswordRecoveryRequestTransfer model = null;

            // Act
            service.SendRecoveryCode(
                model);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ConfirmRecoveryCode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RecoveryService? service = this.CreateService();
            PasswordRecoveryConfirmTransfer model = null;

            // Act
            Domain.ValueObjects.RecoveryCode? result = service.ConfirmRecoveryCode(
                model);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ChangePassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RecoveryService? service = this.CreateService();
            PasswordRecoveryTransfer model = null;

            // Act
            bool result = service.ChangePassword(
                model);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}