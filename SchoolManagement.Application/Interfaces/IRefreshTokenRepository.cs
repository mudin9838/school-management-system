using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task SaveChangesAsync();
}