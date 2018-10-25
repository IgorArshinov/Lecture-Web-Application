using LectureWebApplication.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LectureWebApplication.Persistence.EntityConfigurations
{
    public class LectureConfiguration : EntityTypeConfiguration<Lecture>
    {
        public LectureConfiguration()
        {
            Property(u => u.UserId)
                .IsRequired();

            Property(d => d.DepartmentId)
                .IsRequired();

            Property(s => s.Subject)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(a => a.Attendances)
                .WithRequired(a => a.Lecture)
                .WillCascadeOnDelete(false);
        }
    }
}