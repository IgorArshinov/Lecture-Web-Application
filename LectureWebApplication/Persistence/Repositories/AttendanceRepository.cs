using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IApplicationDbContext _context;

        public AttendanceRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Lecture.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendanceByUserIdAndLectureId(long lectureId, string userId)
        {
            return _context.Attendances.SingleOrDefault(a => a.LectureId == lectureId && a.AttendeeId == userId);
        }

        public void AddAttendance(Attendance newAttendance)
        {
            _context.Attendances.Add(newAttendance);
        }

        public void RemoveAttendance(Attendance attendanceToRemove)
        {
            _context.Attendances.Remove(attendanceToRemove);
        }
    }
}