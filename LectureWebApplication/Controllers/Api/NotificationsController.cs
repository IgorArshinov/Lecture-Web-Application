using LectureWebApplication.Core;
using LectureWebApplication.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace LectureWebApplication.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            return _unitOfWork.NotificationRepository.GetListWithAllUnreadNotificationsCreatedByUser(userId)
                .Select(n => new NotificationDto
                {
                    DateTime = n.DateTime,
                    Lecture = new LectureDto
                    {
                        User = new ApplicationUserDto
                        {
                            Id = n.Lecture.User.Id,
                            Name = n.Lecture.User.Name
                        },
                        DateTime = n.Lecture.DateTime,
                        Id = n.Lecture.Id,
                        IsCanceled = n.Lecture.IsCanceled,
                        Subject = n.Lecture.Subject,
                        Department = new DepartmentDto
                        {
                            Id = n.Lecture.Department.Id,
                            Name = n.Lecture.Department.Name
                        }
                    },
                    OriginalDepartment = n.OriginalDepartment,
                    OriginalDateTime = n.OriginalDateTime,
                    OriginalSubject = n.OriginalSubject,
                    UpdatedDepartment = n.UpdatedDepartment,
                    UpdatedDateTime = n.UpdatedDateTime,
                    UpdatedSubject = n.UpdatedSubject,
                    Type = n.Type
                });
        }

        [HttpPost]
        public IHttpActionResult ChangeIsReadToTrue()
        {
            var userId = User.Identity.GetUserId();

            _unitOfWork.UserNotificationRepository.GetListWithAllUnreadUserNotificationsForUser(userId)
                .ForEach(n => n.Read());
            _unitOfWork.Complete();

            return Ok();
        }
    }
}