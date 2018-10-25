using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendanceByUserIdAndLectureId(long lectureId, string userId);
        void AddAttendance(Attendance newAttendance);
        void RemoveAttendance(Attendance attendanceToRemove);
    }
}