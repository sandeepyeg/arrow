using ArrowDrivingSchool.Domain.Entities;
using ArrowDrivingSchool.Infrastructure.Data;
using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArrowDrivingSchool.Infrastructure.Repositories;

public class StudentRepository(AppDbContext context) : IStudentRepository
{
    public async Task AddAsync(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student != null)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Student>> GetAllAsync() =>
        await context.Students.Include(s => s.Driver).ToListAsync();

    public async Task<Student?> GetByIdAsync(int id) =>
        await context.Students.Include(s => s.Driver).FirstOrDefaultAsync(x => x.Id == id);

    public async Task UpdateAsync(Student student)
    {
        context.Students.Update(student);
        await context.SaveChangesAsync();
    }
}