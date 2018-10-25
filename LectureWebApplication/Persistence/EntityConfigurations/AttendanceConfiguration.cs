using LectureWebApplication.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LectureWebApplication.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { LectureId = a.LectureId, a.AttendeeId });
        }
    }
}