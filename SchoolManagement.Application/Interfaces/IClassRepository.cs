using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IClassRepository
{
    Task AddAsync(Class classEntity);
    Task<Class?> GetByIdAsync(Guid id);
    Task<List<Class>> GetAllAsync();
}
