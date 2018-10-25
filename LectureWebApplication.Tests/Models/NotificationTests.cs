using FluentAssertions;
using LectureWebApplication.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LectureWebApplication.Tests.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void UpdateLecture_WhenCalled_ShouldReturnedNotificationForUpdatedLecture()
        {
            var lecture = new Lecture();
            var notificationForUpdatedLecture = Notification.UpdateLecture(lecture, DateTime.Now.AddDays(1), "a", "z", DateTime.Now.AddDays(2), "az", "za");

            notificationForUpdatedLecture.Type.Should().Be(NotificationType.LectureUpdated);
            notificationForUpdatedLecture.Lecture.Should().Be(lecture);

        }
    }
}