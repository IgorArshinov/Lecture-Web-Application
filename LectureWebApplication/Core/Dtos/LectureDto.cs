using System;

namespace LectureWebApplication.Core.Dtos
{
    public class LectureDto
    {
        public long Id { get; set; }
        public bool IsCanceled { get; set; }
        public ApplicationUserDto User { get; set; }
        public DateTime DateTime { get; set; }
        public string Subject { get; set; }
        public DepartmentDto Department { get; set; }
    }
}