namespace LectureWebApplication.Core.Models
{
    public class Attendance
    {
        public Lecture Lecture { get; set; }
        public ApplicationUser Attendee { get; set; }
        public long LectureId { get; set; }
        public string AttendeeId { get; set; }
    }
}