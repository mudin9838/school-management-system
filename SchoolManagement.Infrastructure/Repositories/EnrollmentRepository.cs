using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly SchoolDbContext _context;
    public EnrollmentRepository(SchoolDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ExistsAsync(Guid studentId, Guid classId)
    {
        return await _context.Enrollments.AnyAsync(x => x.StudentId == studentId && x.ClassId == classId);
    }
    public async Task AddAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
    }

}
