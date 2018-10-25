using LectureWebApplication.Core.Models;
using System;

namespace LectureWebApplication.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalSubject { get; set; }
        public string OriginalDepartment { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string UpdatedSubject { get; set; }
        public string UpdatedDepartment { get; set; }
        public LectureDto Lecture { get; set; }
    }
}