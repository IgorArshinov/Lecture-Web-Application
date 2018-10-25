using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Core.Repositories
{
    public interface ILectureRepository
    {
        IEnumerable<Lecture> GetLecturesUserIsAttendingTo(string userId);
        Lecture GetLectureByIdWithAttendees(long lectureId);
        IEnumerable<Lecture> GetAllLecturesByUserId(string userId);
        Lecture GetLectureById(long lectureId);
        void AddLecture(Lecture lecture);
        IEnumerable<Lecture> GetAllLectures();
    }
}