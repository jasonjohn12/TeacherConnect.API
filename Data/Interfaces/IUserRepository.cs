using System.Threading.Tasks;
using TeacherConnect.Dto;
using TeacherConnect.Entities;

namespace TeacherConnect.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUser(AppUser user);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByIdAsync(int userid);
        Task<int> LogUser(AppUser user);
    }
}