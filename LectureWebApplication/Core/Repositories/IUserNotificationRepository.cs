using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetListWithAllUnreadUserNotificationsForUser(string userId);
    }
}