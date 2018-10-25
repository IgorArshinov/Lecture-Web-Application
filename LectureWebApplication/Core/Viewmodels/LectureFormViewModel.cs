using LectureWebApplication.Controllers;
using LectureWebApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LectureWebApplication.Core.Viewmodels
{
    public class LectureFormViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [FutureDateValidator]
        public string Date { get; set; }

        [Required]
        [TimeValidator]
        public string Time { get; set; }

        [Required]
        public long Department { get; set; }

        public IEnumerable<Department> Departments { get; set; }
        public string Description { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<LecturesController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<LecturesController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}", CultureInfo.CurrentCulture);
        }
    }
}