using LectureWebApplication.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LectureWebApplication.Persistence.EntityConfigurations
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}