using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface ITeacherRepository
{
    Task AddAsync(Teacher teacher);
    Task<List<Teacher>> GetAllAsync();
}
