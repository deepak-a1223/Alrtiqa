using API.DTOs;

namespace API.Interfaces
{
    public interface IStudentRepositry
    {
        Task<IEnumerable<StudentDto>> GetAllStudents();
        Task<bool> AddStudent(StudentDto studentDto);
    }
}
