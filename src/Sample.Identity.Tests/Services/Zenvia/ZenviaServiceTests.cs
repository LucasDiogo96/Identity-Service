//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Moq;
//using NUnit.Framework;
//using Sample.Identity.Infra.Services.Zenvia;
//using Sample.Identity.Infra.Services.Zenvia.Models;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Sample.Identity.Tests.Services.Zenvia
//{
//    [TestFixture]
//    public class ZenviaServiceTests
//    {
//        private MockRepository mockRepository;

//        private Mock<IOptions<ZenviaSettings>> mockOptions;
//        private Mock<IHttpClientFactory> mockHttpClientFactory;
//        private Mock<ILogger<ZenviaService>> mockLogger;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);

//            this.mockOptions = this.mockRepository.Create<IOptions<ZenviaSettings>>();
//            this.mockHttpClientFactory = this.mockRepository.Create<IHttpClientFactory>();
//            this.mockLogger = this.mockRepository.Create<ILogger<ZenviaService>>();
//        }

//        private ZenviaService CreateService()
//        {
//            return new ZenviaService(
//                this.mockOptions.Object,
//                this.mockHttpClientFactory.Object,
//                this.mockLogger.Object);
//        }

//        [Test]
//        public async Task SendAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            ZenviaService? service = this.CreateService();
//            string phone = null;
//            string message = null;

//            // Act
//            await service.SendAsync(
//                phone,
//                message);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}