using LectureWebApplication.Core.Models;
using System.Collections.Generic;

namespace LectureWebApplication.Persistence.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetAllOtherUsers(string userId);
    }
}