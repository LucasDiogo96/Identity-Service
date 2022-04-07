using System;
using Moq;
using NUnit.Framework;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Tests.ValueObjects
{
    [TestFixture]
    public class RecoveryCodeTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private RecoveryCode CreateRecoveryCode()
        {
            return new RecoveryCode(15);
        }

        [Test]
        public void RecoveryCode_NewRecovery_NotEmptyIdentifier()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            Assert.IsNotEmpty(recoveryCode.Identifier);
        }

        [Test]
        public void RecoveryCode_NewRecovery_NotEmptyCode()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            Assert.IsNotEmpty(recoveryCode.Code);
        }

        [Test]
        public void RecoveryCode_NewRecovery_ExpiresGreaterThanNow()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            Assert.Greater(recoveryCode.ExpiresOn, DateTime.UtcNow);
        }

        [Test]
        public void RecoveryCode_NewRecovery_CodeCanBeConvertedToInt()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            int value = int.Parse(recoveryCode.Code);

            Assert.Greater(value, 0);
        }

        [Test]
        public void Equals_NonEqualsCode_MustBeFalse()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            string code = "1234";

            Assert.IsFalse(recoveryCode.Equals(code));
        }

        [Test]
        public void Equals_EqualsCode_MustBeTrue()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            string code = recoveryCode.Code;

            Assert.IsTrue(recoveryCode.Equals(code));
        }

        [Test]
        public void Inactivate_ActiveCode_ActiveMustBeFalse()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            recoveryCode.Inactivate();

            Assert.IsFalse(recoveryCode.Active);
        }

        [Test]
        public void Inactivate_InactiveCode_ActiveMustBeFalse()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            recoveryCode.Active = false;

            recoveryCode.Inactivate();

            Assert.IsFalse(recoveryCode.Active);
        }

        [Test]
        public void Verify_VerifiedOnNull_MustNotBeNull()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            recoveryCode.Verify();

            Assert.NotNull(recoveryCode.VerifiedOn);
        }

        [Test]
        public void Verify_WithExpiredCode_MustThrowInvalidOperationException()
        {
            // Arrange
            RecoveryCode recoveryCode = this.CreateRecoveryCode();

            recoveryCode.ExpiresOn = recoveryCode.ExpiresOn.AddMinutes(-30);

            Assert.Throws<InvalidOperationException>(() => recoveryCode.Verify());
        }
    }
}