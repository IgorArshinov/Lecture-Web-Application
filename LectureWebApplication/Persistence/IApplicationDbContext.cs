using LectureWebApplication.Core.Models;
using System.Data.Entity;

namespace LectureWebApplication.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Lecture> Lectures { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}