using System;

namespace LectureWebApplication.Core.Models
{
    public class UserNotification
    {
        public string ApplicationUserId { get; private set; }
        public int NotificationId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }
        public Notification Notification { get; private set; }
        public bool IsRead { get; private set; }

        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser applicationUser, Notification notification)
        {
            if (applicationUser == null)
                throw new ArgumentNullException("applicationUser");

            if (notification == null)
                throw new ArgumentNullException("notification");

            ApplicationUser = applicationUser;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}