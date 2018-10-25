using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LectureWebApplication.Core.Models
{
    public class Lecture
    {
        public long Id { get; set; }
        public bool IsCanceled { get; private set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Subject { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public Lecture()
        {
            Attendances = new Collection<Attendance>();
        }

        public void LectureIsCreated(IEnumerable<ApplicationUser> allOtherApplicationUsers)
        {
            var createNotification = Notification.CreateLecture(this);

            foreach (var applicationUser in allOtherApplicationUsers)
            {
                applicationUser.Notify(createNotification);
            }
        }

        public void LectureIsCanceled()
        {
            IsCanceled = true;

            var cancelNotification = Notification.CancelLecture(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(cancelNotification);
            }
        }

        public void LectureIsUpdated(DateTime updatedDateTime, Department updatedDepartment, string updatedSubject)
        {
            if (updatedDateTime.Equals(DateTime) && updatedDepartment.Id.Equals(DepartmentId) && updatedSubject.Equals(Subject))
                return;

            string originalDepartment = this.Department.Name;
            DateTime originalDateTime = DateTime;
            string originalSubject = Subject;

            Subject = updatedSubject;
            DateTime = updatedDateTime;
            DepartmentId = updatedDepartment.Id;

            var updateNotification = Notification.UpdateLecture(this, originalDateTime, originalSubject, originalDepartment, updatedDateTime, updatedSubject, updatedDepartment.Name);

            foreach (var applicationUser in Attendances.Select(a => a.Attendee))
            {
                applicationUser.Notify(updateNotification);
            }
        }
    }
}