using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Core.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetListWithAllDepartments();
        Department GetDepartmentById(long departmentId);

    }
}