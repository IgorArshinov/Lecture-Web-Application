using LectureWebApplication.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LectureWebApplication.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(l => l.Lecture);
        }
    }
}