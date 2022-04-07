//using Microsoft.Extensions.Options;
//using Moq;
//using NUnit.Framework;
//using Sample.Identity.Domain.Entities;
//using Sample.Identity.Infra.Models;
//using Sample.Identity.Infra.Providers;
//using System;

//namespace Sample.Identity.Tests.Providers
//{
//    [TestFixture]
//    public class IdentityProviderTests
//    {
//        private MockRepository mockRepository;

//        private Mock<IOptions<AppSettings>> mockOptions;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);

//            this.mockOptions = this.mockRepository.Create<IOptions<AppSettings>>();
//        }

//        private IdentityProvider CreateProvider()
//        {
//            return new IdentityProvider(
//                this.mockOptions.Object);
//        }

//        [Test]
//        public void SignIn_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            IdentityProvider? provider = this.CreateProvider();
//            User user = null;

//            // Act
//            UserIdentity? result = provider.SignIn(
//                user);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}