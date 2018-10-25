using LectureWebApplication.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LectureWebApplication.Core.Viewmodels
{
    public class LecturesViewModel
    {
        public IEnumerable<Lecture> UpcomingLectures { get; set; }
        public bool ShowActions { get; set; }
        public string Description { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<long, Attendance> Attendances { get; set; }
    }
}