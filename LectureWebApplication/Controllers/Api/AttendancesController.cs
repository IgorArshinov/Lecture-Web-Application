using LectureWebApplication.Core;
using LectureWebApplication.Core.Dtos;
using LectureWebApplication.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LectureWebApplication.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendancesDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.AttendanceRepository.GetAttendanceByUserIdAndLectureId(dto.LectureId, userId);
            if (attendance != null)
            {
                return BadRequest("The attendance already exists.");
            }

            var newAttendance = new Attendance
            {
                LectureId = dto.LectureId,
                AttendeeId = userId
            };
            _unitOfWork.AttendanceRepository.AddAttendance(newAttendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(long id)
        {
            var userId = User.Identity.GetUserId();
            var attendanceToRemove = _unitOfWork.AttendanceRepository.GetAttendanceByUserIdAndLectureId(id, userId);
            if (attendanceToRemove == null)
            {
                return NotFound();
            }

            _unitOfWork.AttendanceRepository.RemoveAttendance(attendanceToRemove);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}