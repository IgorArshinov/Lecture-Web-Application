using LectureWebApplication.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LectureWebApplication.Controllers.Api
{
    [Authorize]
    public class LecturesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var lecture = _unitOfWork.LectureRepository.GetLectureByIdWithAttendees(id);

            if (lecture == null || lecture.IsCanceled)
                return NotFound();

            if (!userId.Equals(lecture.UserId))
                return Unauthorized();

            lecture.LectureIsCanceled();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}