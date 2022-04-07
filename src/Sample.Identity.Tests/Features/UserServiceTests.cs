//using Moq;
//using NUnit.Framework;
//using Sample.Identity.App.Features;
//using Sample.Identity.Domain.Commands;
//using Sample.Identity.Domain.Contracts;
//using Sample.Identity.Infra.Contracts;
//using System;
//using System.Threading.Tasks;

//namespace Sample.Identity.Tests.Features
//{
//    [TestFixture]
//    public class UserServiceTests
//    {
//        private MockRepository mockRepository;

//        private Mock<IUnitOfWork> mockUnitOfWork;
//        private Mock<INotification> mockNotification;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);

//            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
//            this.mockNotification = this.mockRepository.Create<INotification>();
//        }

//        private UserService CreateService()
//        {
//            return new UserService(
//                this.mockUnitOfWork.Object,
//                this.mockNotification.Object);
//        }

//        [Test]
//        public async Task Get_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            UserService? service = this.CreateService();
//            string id = null;

//            // Act
//            App.Transfers.User.UserResponseTransfer? result = await service.Get(
//                id);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public void Add_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            UserService? service = this.CreateService();
//            CreateUserCommand model = null;

//            // Act
//            service.Add(
//                model);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public async Task Update_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            UserService? service = this.CreateService();
//            UpdateUserCommand model = null;

//            // Act
//            await service.Update(
//                model);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}