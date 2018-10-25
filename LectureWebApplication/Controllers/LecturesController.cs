using LectureWebApplication.Core;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Viewmodels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace LectureWebApplication.Controllers
{
    public class LecturesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ViewResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new MineViewModel
            {
                AllLecturesCreatedByUser = _unitOfWork.LectureRepository.GetAllLecturesByUserId(userId),
                Description = "My created lectures."
            };

            ViewBag.Current = "Mine";

            return View("Mine", viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new LecturesViewModel
            {
                UpcomingLectures = _unitOfWork.LectureRepository.GetLecturesUserIsAttendingTo(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Description = "Lectures that I'm attending to.",
                Attendances = _unitOfWork.AttendanceRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.LectureId)
            };

            ViewBag.Current = "Attending";

            return View("Lectures", viewModel);
        }

        [HttpPost]
        public ActionResult Search(LecturesViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Current = "Create";

            var viewModel = new LectureFormViewModel
            {
                Departments = _unitOfWork.DepartmentRepository.GetListWithAllDepartments(),
                Description = "Create a lecture by filling all the fields."
            };

            return View("LectureForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var lecture = _unitOfWork.LectureRepository.GetLectureById(id);

            if (lecture == null)
            {
                return HttpNotFound();
            }

            if (lecture.UserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new LectureFormViewModel
            {
                Departments = _unitOfWork.DepartmentRepository.GetListWithAllDepartments(),
                Date = lecture.DateTime.ToString("d MMM yyyy"),
                Time = lecture.DateTime.ToString("HH:mm"),
                Department = lecture.DepartmentId,
                Subject = lecture.Subject,
                Description = "Edit the lecture by filling all the fields.",
                Id = lecture.Id
            };

            return View("LectureForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LectureFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Departments = _unitOfWork.DepartmentRepository.GetListWithAllDepartments();
                return View("LectureForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var lecture = new Lecture
            {
                UserId = userId,
                DateTime = viewModel.GetDateTime(),
                DepartmentId = viewModel.Department,
                Subject = viewModel.Subject
            };

            _unitOfWork.LectureRepository.AddLecture(lecture);
            lecture.LectureIsCreated(_unitOfWork.ApplicationUserRepository.GetAllOtherUsers(userId));
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Lectures");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LectureFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Departments = _unitOfWork.DepartmentRepository.GetListWithAllDepartments();
                return View("LectureForm", viewModel);
            }

            var lecture = _unitOfWork.LectureRepository.GetLectureByIdWithAttendees(viewModel.Id);
            if (lecture == null)
            {
                return HttpNotFound();
            }

            if (lecture.UserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            lecture.LectureIsUpdated(viewModel.GetDateTime(), _unitOfWork.DepartmentRepository.GetDepartmentById(viewModel.Department), viewModel.Subject);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Lectures");
        }

        public ActionResult Details(int id)
        {
            var lecture = _unitOfWork.LectureRepository.GetLectureById(id);

            if (lecture == null)
            {
                return HttpNotFound();
            }

            var viewModel = new LectureDetailsViewModel
            {
                Lecture = lecture
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _unitOfWork.AttendanceRepository.GetAttendanceByUserIdAndLectureId(lecture.Id, userId) != null;
            }

            return View("Details", viewModel);
        }
    }
}