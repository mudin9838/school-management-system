using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IEnrollmentRepository
{
    Task<bool> ExistsAsync(Guid studentId, Guid classId);
    Task AddAsync(Enrollment enrollment);
}

