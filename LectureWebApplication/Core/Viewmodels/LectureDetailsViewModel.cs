using LectureWebApplication.Core.Models;

namespace LectureWebApplication.Core.Viewmodels
{
    public class LectureDetailsViewModel
    {
        public Lecture Lecture { get; set; }
        public bool IsAttending { get; set; }
    }
}