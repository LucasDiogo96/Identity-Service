using Moq;
using NUnit.Framework;
using Sample.Identity.App.Features;
using Sample.Identity.Infra.Contracts;
using System;
using System.Threading.Tasks;

namespace Sample.Identity.Tests.Features
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ISmsService> mockSmsService;
        private Mock<IEmailService> mockEmailService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSmsService = this.mockRepository.Create<ISmsService>();
            this.mockEmailService = this.mockRepository.Create<IEmailService>();
        }

        private NotificationService CreateService()
        {
            return new NotificationService(
                this.mockSmsService.Object,
                this.mockEmailService.Object);
        }

        [Test]
        public async Task SendIdentityConfirmSms_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationService? service = this.CreateService();
            string code = null;
            string phone = null;

            // Act
            await service.SendIdentityConfirmSms(
                code,
                phone);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public async Task SendRecoverySms_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationService? service = this.CreateService();
            string code = null;
            string phone = null;

            // Act
            await service.SendRecoverySms(
                code,
                phone);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public async Task SendRecoveryEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationService? service = this.CreateService();
            string code = null;
            string email = null;
            string name = null;

            // Act
            await service.SendRecoveryEmail(
                code,
                email,
                name);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public async Task SendIdentityConfirmEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationService? service = this.CreateService();
            string code = null;
            string email = null;

            // Act
            await service.SendIdentityConfirmEmail(
                code,
                email);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}