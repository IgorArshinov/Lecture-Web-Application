using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetListWithAllUnreadUserNotificationsForUser(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.ApplicationUserId == userId && !u.IsRead)
                .ToList();
        }
    }
}