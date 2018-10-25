using LectureWebApplication.Core;
using LectureWebApplication.Core.Viewmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LectureWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            ViewBag.Current = "Index";

            var upcomingLectures = _unitOfWork.LectureRepository.GetAllLectures();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingLectures = upcomingLectures
                    .Where(g => g.User.Name.Contains(query) ||
                                g.Department.Name.Contains(query) ||
                                g.Subject.Contains(query));
            }

            var attendances = _unitOfWork.AttendanceRepository.GetFutureAttendances(User.Identity.GetUserId())
                .ToLookup(a => a.LectureId);

            var viewModel = new LecturesViewModel
            {
                UpcomingLectures = upcomingLectures,
                ShowActions = User.Identity.IsAuthenticated,
                Description = "All lectures created by users are listed here. You can search a lecture by it's username, department or subject.",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Lectures", viewModel);
        }
    }
}