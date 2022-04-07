//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Moq;
//using NUnit.Framework;
//using Sample.Identity.Infra.Services.Sendgrid;
//using Sample.Identity.Infra.Services.Sendgrid.Models;
//using SendGrid;
//using System;
//using System.Threading.Tasks;

//namespace Sample.Identity.Tests.Services.Sendgrid
//{
//    [TestFixture]
//    public class SendGridServiceTests
//    {
//        private MockRepository mockRepository;

//        private Mock<ISendGridClient> mockSendGridClient;
//        private Mock<IOptions<SendgridSettings>> mockOptions;
//        private Mock<ILogger<SendGridService>> mockLogger;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);

//            this.mockSendGridClient = this.mockRepository.Create<ISendGridClient>();
//            this.mockOptions = this.mockRepository.Create<IOptions<SendgridSettings>>();
//            this.mockLogger = this.mockRepository.Create<ILogger<SendGridService>>();
//        }

//        private SendGridService CreateService()
//        {
//            return new SendGridService(
//                this.mockSendGridClient.Object,
//                this.mockOptions.Object,
//                this.mockLogger.Object);
//        }

//        [Test]
//        public async Task SendAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            SendGridService? service = this.CreateService();
//            string email = null;
//            string subject = null;
//            string message = null;

//            // Act
//            await service.SendAsync(
//                email,
//                subject,
//                message);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}