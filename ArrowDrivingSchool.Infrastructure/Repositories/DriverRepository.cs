using ArrowDrivingSchool.Domain.Entities;
using ArrowDrivingSchool.Infrastructure.Data;
using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArrowDrivingSchool.Infrastructure.Repositories;

public class DriverRepository(AppDbContext context) : IDriverRepository
{
    public async Task AddAsync(Driver driver)
    {
        context.Drivers.Add(driver);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var driver = await context.Drivers.FindAsync(id);
        if (driver != null)
        {
            context.Drivers.Remove(driver);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Driver>> GetAllAsync() =>
        await context.Drivers.Include(d => d.Students).ToListAsync();

    public async Task<Driver?> GetByIdAsync(int id) =>
        await context.Drivers.Include(d => d.Students).FirstOrDefaultAsync(x => x.Id == id);

    public async Task UpdateAsync(Driver driver)
    {
        context.Drivers.Update(driver);
        await context.SaveChangesAsync();
    }
}