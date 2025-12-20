using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly SchoolDbContext _context;
    public ClassRepository(SchoolDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Class classEntity)
    {
        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Class>> GetAllAsync()
    {
        return await _context.Classes.ToListAsync();
    }

    public async Task<Class?> GetByIdAsync(Guid id)
    {
        return await _context.Classes.FirstOrDefaultAsync(x => x.Id == id);
    }
}
