using LectureWebApplication.Core;
using LectureWebApplication.Core.Repositories;
using LectureWebApplication.Persistence.Repositories;

namespace LectureWebApplication.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAttendanceRepository AttendanceRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public ILectureRepository LectureRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }
        public IUserNotificationRepository UserNotificationRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUserRepository = new ApplicationUserRepository(context);
            LectureRepository = new LectureRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            AttendanceRepository = new AttendanceRepository(context);
            NotificationRepository = new NotificationRepository(context);
            UserNotificationRepository = new UserNotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}