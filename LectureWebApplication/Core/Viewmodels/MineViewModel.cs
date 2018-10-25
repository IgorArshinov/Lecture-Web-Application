using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Core.Viewmodels
{
    public class MineViewModel
    {
        public IEnumerable<Lecture> AllLecturesCreatedByUser { get; set; }
        public string Description { get; set; }
    }
}