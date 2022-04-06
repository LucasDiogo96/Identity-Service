using Moq;
using NUnit.Framework;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.ValueObjects;
using System;

namespace Sample.Identity.Tests.Entities
{
    [TestFixture]
    public class UserTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private User CreateUser()
        {
            return new User();
        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();
            string firstname = null;
            string lastname = null;
            string phoneNumber = null;
            string culture = null;
            string password = null;

            // Act
            user.Update(
                firstname,
                lastname,
                phoneNumber,
                culture,
                password);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Block_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.Block();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Unlock_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.Unlock();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Inactivate_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.Inactivate();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ResetSignInAttempts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.ResetSignInAttempts();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void OnFailedSignInAttempt_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.OnFailedSignInAttempt();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ForgotPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();
            RecoveryCode code = null;

            // Act
            user.ForgotPassword(
                code);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ChangePassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();
            string password = null;

            // Act
            user.ChangePassword(
                password);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ConfirmEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.ConfirmEmail();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ConfirmPhone_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            User? user = this.CreateUser();

            // Act
            user.ConfirmPhone();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}