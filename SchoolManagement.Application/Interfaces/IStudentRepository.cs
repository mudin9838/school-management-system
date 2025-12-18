using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task AddAsync(Student student);

}
