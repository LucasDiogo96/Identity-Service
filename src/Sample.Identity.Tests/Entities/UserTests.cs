using GenFu;
using Moq;
using NUnit.Framework;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.ValueObjects;
using System;
using System.Globalization;
using System.IO;

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
            Password password = new Password("5gcRFs%#PW%c");

            GenFu.GenFu.Configure<User>()
                .Fill(e => e.Id, Guid.NewGuid().ToString())
                .Fill(e => e.Trustable, true)
                .Fill(e => e.FirstName, "Bruce")
                .Fill(e => e.LastName, "Wayne")
                .Fill(e => e.PhoneNumber, "5559072000775")
                .Fill(e => e.Active, true)
                .Fill(e => e.AccessFailedCount, 0)
                .Fill(e => e.Blocked, false)
                .Fill(e => e.EmailConfirmed, true)
                .Fill(e => e.PhoneNumberConfirmed, true)
                .Fill(e => e.Password, password)
                .Fill(e => e.PasswordRecoveries, A.ListOf<RecoveryCode>(0));

            return A.New<User>();
        }

        [Test]
        public void Block_NotBlockedUser_BlockMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Block();

            Assert.IsTrue(user.Blocked);
        }

        [Test]
        public void Block_BlockeduUser_BlockMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Blocked = true;

            user.Block();

            Assert.IsTrue(user.Blocked);
        }

        [Test]
        public void Block_UserEqualsNull_MustThrowsNullReferenceException()
        {
            // Arrange
            User user = null;

            Assert.Throws<NullReferenceException>(() => user.Block());
        }

        [Test]
        public void Unlock_BlockedUser_BlockMustBeFalse()
        {
            // Arrange
            User user = this.CreateUser();

            user.Block();

            user.Unlock();

            Assert.IsFalse(user.Blocked);
        }

        [Test]
        public void Unlock_NonBlockedUser_BlockMustBeFalse()
        {
            // Arrange
            User user = this.CreateUser();

            user.Unlock();

            Assert.IsFalse(user.Blocked);
        }

        [Test]
        public void Unlock_UserEqualsNull_MustThrowsNullReferenceException()
        {
            // Arrange
            User user = null;

            Assert.Throws<NullReferenceException>(() => user.Unlock());
        }

        [Test]
        public void Inactivate_ActiveUser_ActiveMustBeFalse()
        {
            // Arrange
            User user = this.CreateUser();

            user.Inactivate();

            Assert.False(user.Active);
        }

        [Test]
        public void Inactivate_InactiveUser_ActiveMustBeFalse()
        {
            // Arrange
            User user = this.CreateUser();

            user.Active = false;

            user.Inactivate();

            Assert.False(user.Active);
        }

        [Test]
        public void Inactivate_UserEqualsNull_MustThrowsNullReferenceException()
        {
            // Arrange
            User user = null;

            Assert.Throws<NullReferenceException>(() => user.Inactivate());
        }

        [Test]
        public void ResetSignInAttempts_AttemptsEqualsTen_AccessFailedCountMustBeZero()
        {
            // Arrange
            User user = this.CreateUser();

            user.AccessFailedCount = 10;

            user.ResetSignInAttempts();

            Assert.AreEqual(user.AccessFailedCount, 0);
        }

        [Test]
        public void ResetSignInAttempts_AttemptsEqualsFive_AccessFailedCountMustBeZero()
        {
            // Arrange
            User user = this.CreateUser();

            user.AccessFailedCount = 5;

            user.ResetSignInAttempts();

            Assert.AreEqual(user.AccessFailedCount, 0);
        }

        [Test]
        public void ResetSignInAttempts_AttemptsEqualsZero_AccessFailedCountMustBeZero()
        {
            // Arrange
            User user = this.CreateUser();

            user.AccessFailedCount = 0;

            user.ResetSignInAttempts();

            Assert.AreEqual(user.AccessFailedCount, 0);
        }

        [Test]
        public void ResetSignInAttempts_FilledLockoutEndDateUtc_MustBeNull()
        {
            // Arrange
            User user = this.CreateUser();

            user.LockoutEndDateUtc = DateTime.Today;

            user.ResetSignInAttempts();

            Assert.IsNull(user.LockoutEndDateUtc);
        }

        [Test]
        public void OnFailedSignInAttempt_AccessFailedCountEqualsZero_AccessFailedCountMustBeEqualsOne()
        {
            // Arrange
            User user = this.CreateUser();

            user.OnFailedSignInAttempt();

            Assert.AreEqual(user.AccessFailedCount, 1);
        }

        [Test]
        public void OnFailedSignInAttempt_AccessFailedCountEqualsFive_AccessFailedCountMustBeEqualsSix()
        {
            // Arrange
            User user = this.CreateUser();

            user.AccessFailedCount = 5;

            user.OnFailedSignInAttempt();

            Assert.AreEqual(user.AccessFailedCount, 6);
        }

        [Test]
        public void OnFailedSignInAttempt_AccessFailedCountEqualsTen_AccessFailedCountMustBeEqualsEleven()
        {
            // Arrange
            User user = this.CreateUser();

            user.AccessFailedCount = 10;

            user.OnFailedSignInAttempt();

            Assert.AreEqual(user.AccessFailedCount, 11);
        }

        [Test]
        public void ForgotPassword_NullRecoveryObject_MustThrowsNullReferenceException()
        {
            // Arrange
            User user = this.CreateUser();

            RecoveryCode code = null;

            Assert.Throws<NullReferenceException>(() => user.ForgotPassword(code));
        }

        [Test]
        public void ForgotPassword_ExpiredRecovery_MustThrowsInvalidOperationException()
        {
            // Arrange
            User user = this.CreateUser();

            RecoveryCode code = new(15);

            code.ExpiresOn = DateTime.UtcNow.AddMinutes(-30);

            Assert.Throws<InvalidOperationException>(() => user.ForgotPassword(code));
        }

        [Test]
        public void ForgotPassword_ValidRecovery_PasswordRecoveriesMusBeGreaterThanZero()
        {
            // Arrange
            User user = this.CreateUser();

            RecoveryCode code = new(15);

            user.ForgotPassword(code);

            Assert.Greater(user.PasswordRecoveries.Count, 0);
        }

        [Test]
        public void ConfirmEmail_EmailConfirmedEqualsFalse_EmailConfirmedMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.EmailConfirmed = false;

            user.ConfirmEmail();

            Assert.IsTrue(user.EmailConfirmed);
        }

        [Test]
        public void ConfirmEmail_EmailConfirmedEqualsTrue_EmailConfirmedMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.EmailConfirmed = true;

            user.ConfirmEmail();

            Assert.IsTrue(user.EmailConfirmed);
        }

        [Test]
        public void ConfirmEmail_TrustableEqualsFalse_TrustableMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Trustable = false;

            user.ConfirmEmail();

            Assert.IsTrue(user.Trustable);
        }

        [Test]
        public void ConfirmEmail_TrustableEqualsTrue_TrustableMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Trustable = true;

            user.ConfirmEmail();

            Assert.IsTrue(user.Trustable);
        }

        [Test]
        public void ConfirmPhone_PhoneNumberConfirmedEqualsFalse_PhoneNumberConfirmedMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.PhoneNumberConfirmed = false;

            user.ConfirmPhone();

            Assert.IsTrue(user.PhoneNumberConfirmed);
        }

        [Test]
        public void ConfirmPhone_PhoneNumberConfirmedEqualsTrue_PhoneNumberConfirmedMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.PhoneNumberConfirmed = true;

            user.ConfirmPhone();

            Assert.IsTrue(user.PhoneNumberConfirmed);
        }

        [Test]
        public void ConfirmPhone_TrustableEqualsFalse_TrustableMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Trustable = false;

            user.ConfirmPhone();

            Assert.IsTrue(user.Trustable);
        }

        [Test]
        public void ConfirmPhone_TrustableEqualsTrue_TrustableMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            user.Trustable = true;

            user.ConfirmPhone();

            Assert.IsTrue(user.Trustable);
        }

        [Test]
        public void ChangePassword_PasswordEqualsNull_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = null;

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_PasswordEqualsEmpty_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_OnlyNumbers_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "12345678";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_OnlyLowercaseLetters_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "abcdefgh";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_OnlyUppercaseLetters_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "ABCDEFGH";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_OnlySymbols_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "!@#$%¨&*";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_LessThanEightCharacters_MustThrowInvalidDataException()
        {
            // Arrange
            User user = this.CreateUser();

            string actualSalt = user.Password.Salt;

            string password = "BdU@o8Z";

            Assert.Throws<InvalidDataException>(() => user.ChangePassword(password));
        }

        [Test]
        public void ChangePassword_ValidPassowrd_HashMustChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualHash = user.Password.Hash;

            string password = "BdU@o8Zs";

            user.ChangePassword(password);

            Assert.AreNotSame(user.Password.Hash, actualHash);
        }

        [Test]
        public void ChangePassword_ValidPassowrd_SaltMustChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualSalt = user.Password.Salt;

            string password = "BdU@o8Zs";

            user.ChangePassword(password);

            Assert.AreNotSame(user.Password.Salt, actualSalt);
        }

        [Test]
        public void Update_NullFirstName_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualFirstName = user.FirstName;

            user.Update("", null, null, null, null);

            Assert.AreEqual(actualFirstName, user.FirstName);
        }

        [Test]
        public void Update_EmptyFirstName_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualFirstName = user.FirstName;

            user.Update("", null, null, null, null);

            Assert.AreEqual(actualFirstName, user.FirstName);
        }

        [Test]
        public void Update_NullLastName_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualLastName = user.LastName;

            user.Update(null, null, null, null, null);

            Assert.AreEqual(actualLastName, user.LastName);
        }

        [Test]
        public void Update_EmptyLastName_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string actualLastName = user.LastName;

            user.Update(null, "", null, null, null);

            Assert.AreEqual(actualLastName, user.LastName);
        }

        [Test]
        public void Update_NullPhoneNumber_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string phone = user.PhoneNumber;

            user.Update(null, null, "", null, null);

            Assert.AreEqual(phone, user.PhoneNumber);
        }

        [Test]
        public void Update_EmptyPhoneNumber_MustNotChange()
        {
            // Arrange
            User user = this.CreateUser();

            string phone = user.PhoneNumber;

            user.Update(null, null, "", null, null);

            Assert.AreEqual(phone, user.PhoneNumber);
        }

        [Test]
        public void Update_InvalidCultureCode_MustThrowCultureNotFoundException()
        {
            // Arrange
            User user = this.CreateUser();

            string culture = "ça-va";

            Assert.Throws<CultureNotFoundException>(() => user.Update(null, null, null, culture, null));
        }

        [Test]
        public void Update_ValidCultureCode_MustChange()
        {
            // Arrange
            User user = this.CreateUser();

            string culture = "pt-BR";

            user.Update(null, null, null, culture, null);

            Assert.AreEqual(user.CultureCode, culture);
        }

        [Test]
        public void Update_ValidPassowrd_VerifyMustBeTrue()
        {
            // Arrange
            User user = this.CreateUser();

            string password = "BdU@o8Zs";

            user.Update(null, null, null, null, password);

            Assert.IsTrue(user.Password.Verify(password));
        }
    }
}