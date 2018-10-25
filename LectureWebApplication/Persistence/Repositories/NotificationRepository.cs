using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetListWithAllUnreadNotificationsCreatedByUser(string userId)
        {
            return _context.UserNotifications
                .Where(a => a.ApplicationUserId == userId && !a.IsRead)
                .Select(n => n.Notification)
                .Include(u => u.Lecture.User)
                .Include(d => d.Lecture.Department)
                .ToList();
        }
    }
}