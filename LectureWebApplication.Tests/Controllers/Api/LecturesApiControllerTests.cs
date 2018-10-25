using FluentAssertions;
using LectureWebApplication.Controllers.Api;
using LectureWebApplication.Core;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using LectureWebApplication.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LectureWebApplication.Tests.Controllers.Api
{
    [TestClass]
    public class LecturesApiControllerTests
    {
        private LecturesController _controller;
        private string _userId;
        private Mock<ILectureRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<ILectureRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.SetupGet(u => u.LectureRepository)
                .Returns(_mockRepository.Object);

            _controller = new LecturesController(mockUnitOfWork.Object);
            _userId = "A";

            _controller.MockCurrentUser(_userId, "test_robot@domain.com");
        }

        [TestMethod]
        public void Cancel_LectureWithGivenIdDoesNotExist_ShouldReturnNull()
        {
            var result = _controller.Cancel(1);

            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_LectureIsCanceled_ShouldReturnNotFound()
        {
            var lecture = new Lecture();
            lecture.LectureIsCanceled();

            _mockRepository.Setup(r => r.GetLectureByIdWithAttendees(1))
                .Returns(lecture);

            var result = _controller.Cancel(1);

            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UnautherizedUser_ShouldReturnUnautherized()
        {
            var lecture = new Lecture { UserId = _userId + "A" };

            _mockRepository.Setup(r => r.GetLectureByIdWithAttendees(1))
                .Returns(lecture);

            var result = _controller.Cancel(1);

            result.Should()
                .BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var lecture = new Lecture { UserId = _userId };

            _mockRepository.Setup(r => r.GetLectureByIdWithAttendees(1))
                .Returns(lecture);

            var result = _controller.Cancel(1);

            result.Should()
                .BeOfType<OkResult>();
        }
    }
}