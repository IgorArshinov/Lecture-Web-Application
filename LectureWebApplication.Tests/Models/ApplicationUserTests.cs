﻿using FluentAssertions;
using LectureWebApplication.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LectureWebApplication.Tests.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            var user = new ApplicationUser();
            var notification = Notification.CancelLecture(new Lecture());

            user.Notify(notification);
            user.UserNotifications.Count.Should().Be(1);

            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.ApplicationUser.Should().Be(user);
        }
    }
}