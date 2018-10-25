using LectureWebApplication.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LectureWebApplication.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IApplicationDbContext _context;

        public ApplicationUserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllOtherUsers(string userId)
        {
            return _context.Users
                .Where(u => u.Id != userId)
                .ToList();
        }
    }
}