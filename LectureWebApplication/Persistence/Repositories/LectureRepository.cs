using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly IApplicationDbContext _context;

        public LectureRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lecture> GetLecturesUserIsAttendingTo(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId)
                .Select(l => l.Lecture)
                .Include(u => u.User)
                .Include(d => d.Department)
                .ToList();
        }

        public Lecture GetLectureByIdWithAttendees(long lectureId)
        {
            return _context.Lectures.Include(a => a.Attendances.Select(at => at.Attendee))
                .Include(d => d.Department)
                .SingleOrDefault(l => l.Id == lectureId);
        }

        public IEnumerable<Lecture> GetAllLecturesByUserId(string userId)
        {
            return _context.Lectures.Where(l => l.UserId == userId && l.DateTime > DateTime.Now && !l.IsCanceled)
                .Include(d => d.Department)
                .ToList();
        }

        public Lecture GetLectureById(long lectureId)
        {
            return _context.Lectures.Include(u => u.User)
                .Include(d => d.Department)
                .SingleOrDefault(l => l.Id == lectureId);
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            return _context.Lectures.Include(u => u.User)
                .Include(d => d.Department)
                .Where(l => l.DateTime > DateTime.Now && !l.IsCanceled);
        }

        public void AddLecture(Lecture lecture)
        {
            _context.Lectures.AddOrUpdate(lecture);
        }
    }
}