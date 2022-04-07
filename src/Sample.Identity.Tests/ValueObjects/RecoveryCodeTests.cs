//using Moq;
//using NUnit.Framework;
//using Sample.Identity.Domain.ValueObjects;
//using System;

//namespace Sample.Identity.Tests.ValueObjects
//{
//    [TestFixture]
//    public class RecoveryCodeTests
//    {
//        private MockRepository mockRepository;

//        [SetUp]
//        public void SetUp()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);
//        }

//        private RecoveryCode CreateRecoveryCode()
//        {
//            return new RecoveryCode();
//        }

//        [Test]
//        public void Verify_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            RecoveryCode? recoveryCode = this.CreateRecoveryCode();

//            // Act
//            recoveryCode.Verify();

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public void Inactivate_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            RecoveryCode? recoveryCode = this.CreateRecoveryCode();

//            // Act
//            recoveryCode.Inactivate();

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }

//        [Test]
//        public void Equals_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            RecoveryCode? recoveryCode = this.CreateRecoveryCode();
//            string code = null;

//            // Act
//            bool result = recoveryCode.Equals(
//                code);

//            // Assert
//            Assert.Fail();
//            this.mockRepository.VerifyAll();
//        }
//    }
//}