using FluentAssertions;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Persistence;
using LectureWebApplication.Persistence.Repositories;
using LectureWebApplication.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;

namespace LectureWebApplication.Tests.Persistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _notificationRepository;
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();

            _notificationRepository = new NotificationRepository(mockContext.Object);

            mockContext.Setup(c => c.UserNotifications)
                .Returns(() => _mockNotifications.Object);
        }

        [TestMethod]
        public void GetListWithAllUnreadNotificationsCreatedByUser_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.CancelLecture(new Lecture());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();
            var listWithNotifications = new List<UserNotification>() { userNotification };
            _mockNotifications.SetSource(listWithNotifications);

            var notifications = _notificationRepository.GetListWithAllUnreadNotificationsCreatedByUser(userNotification.ApplicationUserId);

            notifications.Should()
                .BeEmpty();
        }

        [TestMethod]
        public void GetListWithAllUnreadNotificationsCreatedByUser_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.CancelLecture(new Lecture());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _notificationRepository.GetListWithAllUnreadNotificationsCreatedByUser(userNotification.ApplicationUserId + "a");

            notifications.Should()
                .BeEmpty();
        }

        [TestMethod]
        public void GetListWithAllUnreadNotificationsCreatedByUser_NewNotificationForGivenUser_ShouldBeReturned()
        {
            var notification = Notification.CancelLecture(new Lecture());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            var listWithNotifications = new List<UserNotification>() { userNotification };

            _mockNotifications.SetSource(listWithNotifications);

            var notifications = _notificationRepository.GetListWithAllUnreadNotificationsCreatedByUser(userNotification.ApplicationUserId);

            notifications.Should()
                .HaveCount(1);
            notifications.Should()
                .Contain(notification);
        }
    }
}