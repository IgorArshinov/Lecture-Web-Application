using FluentAssertions;
using LectureWebApplication.Controllers;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Viewmodels;
using LectureWebApplication.IntigrationTests.Extentions;
using LectureWebApplication.Persistence;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace LectureWebApplication.IntigrationTests.Controllers
{
    [TestFixture]
    public class LecturesControllerTests
    {
        private LecturesController _controller;
        private ApplicationDbContext _applicationDbContext;

        [SetUp]
        public void SetUp()
        {
            _applicationDbContext = new ApplicationDbContext();
            _controller = new LecturesController(new UnitOfWork(_applicationDbContext));
        }

        [TearDown]
        public void TearDown()
        {
            _applicationDbContext.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingLectures()
        {
            Debug.Listeners.Add(new DefaultTraceListener());
            var user = _applicationDbContext.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var lecture = new Lecture
            {
                User = user,
                DateTime = DateTime.Now.AddDays(1),
                Department = _applicationDbContext.Departments.First(),
                Subject = "x",
                Id = 1
            };

            _applicationDbContext.Lectures.Add(lecture);
            _applicationDbContext.SaveChanges();

            var result = _controller.Mine();

            (result.ViewData.Model as MineViewModel).AllLecturesCreatedByUser.Should()
                .HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenLecture()
        {
            var user = _applicationDbContext.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var lecture = new Lecture
            {
                User = user,
                DateTime = DateTime.Now.AddDays(1),
                Department = new Department { Name = "x" },
                Subject = "x"
            };
            _applicationDbContext.Lectures.Add(lecture);
            _applicationDbContext.SaveChanges();

            _controller.Update(new LectureFormViewModel
            {
                Id = lecture.Id,
                Date = DateTime.Today.AddMonths(1)
                    .ToString("d MMM yyyy"),
                Time = "12:00",
                Subject = "test",
                Department = 1
            });

            _applicationDbContext.Entry(lecture)
                .Reload();

            lecture.DateTime.Should()
                .Be(DateTime.Today.AddMonths(1)
                    .AddHours(12));
            lecture.Subject.Should()
                .Be("test");
            lecture.DepartmentId.Should()
                .Be(1);
        }
    }
}