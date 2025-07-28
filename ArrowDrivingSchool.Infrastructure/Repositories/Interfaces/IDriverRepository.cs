using ArrowDrivingSchool.Domain.Entities;

namespace ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;

public interface IDriverRepository
{
    Task<List<Driver>> GetAllAsync();
    Task<Driver?> GetByIdAsync(int id);
    Task AddAsync(Driver driver);
    Task UpdateAsync(Driver driver);
    Task DeleteAsync(int id);
}