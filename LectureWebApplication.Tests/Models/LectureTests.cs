using FluentAssertions;
using LectureWebApplication.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LectureWebApplication.Tests.Models
{
    [TestClass]
    public class LectureTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var lecture = new Lecture();

            lecture.LectureIsCanceled();

            lecture.IsCanceled.Should()
                .BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveNotification()
        {
            var lecture = new Lecture();
            lecture.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            lecture.LectureIsCanceled();

            var attendees = lecture.Attendances.Select(a => a.Attendee)
                .ToList();
            attendees[0]
                .UserNotifications.First()
                .Notification.Type.Should()
                .Be(NotificationType.LectureCanceled);
        }
    }
}