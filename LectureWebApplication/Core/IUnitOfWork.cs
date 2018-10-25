using LectureWebApplication.Core.Repositories;
using LectureWebApplication.Persistence.Repositories;

namespace LectureWebApplication.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository AttendanceRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ILectureRepository LectureRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IUserNotificationRepository UserNotificationRepository { get; }
        void Complete();
    }
}