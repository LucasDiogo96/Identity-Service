//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Moq;
//using NUnit.Framework;
//using Sample.Identity.App.Contracts;
//using Sample.Identity.App.Features;
//using Sample.Identity.App.Transfers;
//using Sample.Identity.Domain.Contracts;
//using Sample.Identity.Domain.Enumerators;
//using Sample.Identity.Infra.Contracts;
//using Sample.Identity.Infra.Models;
//using System;
//using System.Threading.Tasks;

//namespace Sample.Identity.Tests.Features
//{
//    [TestFixture]
//    public class IdentityServiceTests
//    {
//        private MockRepository mockRepository;

//        private Mock<IOptions<AppSettings>> mockOptions;
//        private Mock<IIdentityProvider> mockIdentityProvider;
//        private Mock<IUnitOfWork> mockUnitOfWork;
//        private Mock<ILogger<IdentityService>> mockLogger;
//        private Mock<INotification> mockNotification;
//        private Mock<IUserDomainService> mockUserDomainService;
//        private Mock<ICacheManager> mockCacheManager;
//        private Mock<INotificationService> mockNotificationService;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);

//            this.mockOptions = this.mockRepository.Create<IOptions<AppSettings>>();
//            this.mockIdentityProvider = this.mockRepository.Create<IIdentityProvider>();
//            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
//            this.mockLogger = this.mockRepository.Create<ILogger<IdentityService>>();
//            this.mockNotification = this.mockRepository.Create<INotification>();
//            this.mockUserDomainService = this.mockRepository.Create<IUserDomainService>();
//            this.mockCacheManager = this.mockRepository.Create<ICacheManager>();
//            this.mockNotificationService = this.mockRepository.Create<INotificationService>();
//        }

//        private IdentityService CreateService()
//        {
//            return new IdentityService(
//                this.mockOptions.Object,
//                this.mockIdentityProvider.Object,
//                this.mockUnitOfWork.Object,
//                this.mockLogger.Object,
//                this.mockNotification.Object,
//                this.mockUserDomainService.Object,
//                this.mockCacheManager.Object,
//                this.mockNotificationService.Object);
//        }

//        [Test]
//        public void SignIn_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityService? service = this.CreateService();
//            IdentitySignInTransfer model = null;

//            // Act
//            UserIdentity? result = service.SignIn(
//                model);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public void Refresh_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityService? service = this.CreateService();
//            IdentityRefreshTransfer model = null;

//            // Act
//            UserIdentity? result = service.Refresh(
//                model);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public async Task VerifyIdentity_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityService? service = this.CreateService();
//            string userId = null;
//            NotificationType type = default(global::Sample.Identity.Domain.Enumerators.NotificationType);

//            // Act
//            await service.VerifyIdentity(
//                userId,
//                type);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public async Task VerifyEmail_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityService? service = this.CreateService();
//            string userid = null;
//            string verification = null;

//            // Act
//            bool result = await service.VerifyEmail(
//                userid,
//                verification);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public async Task VerifyPhone_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityService? service = this.CreateService();
//            string userid = null;
//            string verification = null;

//            // Act
//            bool result = await service.VerifyPhone(
//                userid,
//                verification);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}