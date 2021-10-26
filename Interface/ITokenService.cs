using TeacherConnect.Entities;

namespace TeacherConnect.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}