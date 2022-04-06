using Moq;
using NUnit.Framework;
using Sample.Identity.Domain.Common;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Domain.Services;
using System;

namespace Sample.Identity.Tests.Services
{
    [TestFixture]
    public class NotificationContextTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private NotificationContext CreateNotificationContext()
        {
            return new NotificationContext();
        }

        [Test]
        public void AddNotification_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();
            MappedErrorsEnum error = default(global::Sample.Identity.Domain.Enumerators.MappedErrorsEnum);

            // Act
            notificationContext.AddNotification(
                error);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void AddNotification_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();
            string key = null;
            string message = null;

            // Act
            notificationContext.AddNotification(
                key,
                message);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void AddNotification_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();
            Notification notification = null;

            // Act
            notificationContext.AddNotification(
                notification);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void HasNotifications_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();

            // Act
            bool result = notificationContext.HasNotifications();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void GetNotifications_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();

            // Act
            System.Collections.Generic.IList<Domain.Common.Notification>? result = notificationContext.GetNotifications();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Exists_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            NotificationContext? notificationContext = this.CreateNotificationContext();
            MappedErrorsEnum error = default(global::Sample.Identity.Domain.Enumerators.MappedErrorsEnum);

            // Act
            bool result = notificationContext.Exists(
                error);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}