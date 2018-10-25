using LectureWebApplication.Core.Models;
using LectureWebApplication.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IApplicationDbContext _context;

        public DepartmentRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetListWithAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartmentById(long departmentId)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == departmentId);
        }
    }
}