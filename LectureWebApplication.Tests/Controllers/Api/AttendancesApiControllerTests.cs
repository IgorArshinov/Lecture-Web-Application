using FluentAssertions;
using LectureWebApplication.Controllers.Api;
using LectureWebApplication.Core;
using LectureWebApplication.Core.Dtos;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using LectureWebApplication.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LectureWebApplication.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesApiControllerTests
    {
        private AttendancesController _attendancesApiController;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.AttendanceRepository)
                .Returns(_mockRepository.Object);

            _attendancesApiController = new AttendancesController(mockUnitOfWork.Object);
            _userId = "1";
            _attendancesApiController.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_UserAttendingLectureThatHeIsAttending_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(a => a.GetAttendanceByUserIdAndLectureId(1, _userId))
                .Returns(attendance);

            var result = _attendancesApiController.Attend(new AttendancesDto { LectureId = 1 });

            result.Should()
                .BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _attendancesApiController.Attend(new AttendancesDto { LectureId = 1 });

            result.Should()
                .BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _attendancesApiController.DeleteAttendance(1);

            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(a => a.GetAttendanceByUserIdAndLectureId(1, _userId))
                .Returns(attendance);

            var result = _attendancesApiController.DeleteAttendance(1);

            result.Should()
                .BeOfType<OkNegotiatedContentResult<long>>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(a => a.GetAttendanceByUserIdAndLectureId(1, _userId))
                .Returns(attendance);

            var result = (OkNegotiatedContentResult<long>)_attendancesApiController.DeleteAttendance(1);

            result.Content.Should()
                .Be(1);
        }
    }
}