using System.Collections.Generic;
using LectureWebApplication.Core.Models;

namespace LectureWebApplication.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetListWithAllUnreadNotificationsCreatedByUser(string userId);
    }
}