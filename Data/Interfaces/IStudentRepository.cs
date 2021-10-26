using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherConnect.Dto;
using TeacherConnect.Entities;

namespace TeacherConnect.Data.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync(int userId);
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<int> AddStudent(Student studentDto);
        // Task<bool> SaveAllAsync();
    }
}