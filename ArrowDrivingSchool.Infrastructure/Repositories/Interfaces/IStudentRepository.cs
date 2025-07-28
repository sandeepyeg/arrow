using ArrowDrivingSchool.Domain.Entities;

namespace ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
}