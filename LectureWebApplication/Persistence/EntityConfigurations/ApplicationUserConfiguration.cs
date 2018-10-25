using LectureWebApplication.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LectureWebApplication.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}