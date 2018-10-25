using System;

namespace LectureWebApplication.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalSubject { get; private set; }
        public string OriginalDepartment { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UpdatedSubject { get; private set; }
        public string UpdatedDepartment { get; private set; }
        public Lecture Lecture { get; private set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Lecture lecture)
        {
            if (lecture == null)
                throw new ArgumentNullException(nameof(lecture));

            Type = type;
            Lecture = lecture;
            DateTime = DateTime.Now;
        }

        public static Notification CreateLecture(Lecture lecture)
        {
            return new Notification(NotificationType.LectureCreated, lecture);
        }

        public static Notification CancelLecture(Lecture lecture)
        {
            return new Notification(NotificationType.LectureCanceled, lecture);
        }

        public static Notification UpdateLecture(Lecture lecture, DateTime dateTime, string subject, string departmentName, DateTime updatedDateTime, string updatedSubject,
            string updatedDepartmentName)
        {
            var updateNotification = new Notification(NotificationType.LectureUpdated, lecture)
            {
                OriginalDateTime = dateTime,
                OriginalSubject = subject,
                OriginalDepartment = departmentName,
                UpdatedDateTime = updatedDateTime,
                UpdatedSubject = updatedSubject,
                UpdatedDepartment = updatedDepartmentName
            };

            return updateNotification;
        }
    }
}