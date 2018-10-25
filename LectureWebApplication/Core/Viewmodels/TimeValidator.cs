using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LectureWebApplication.Core.Viewmodels
{
    public class TimeValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out var validTime);
            return isValid;
        }
    }
}