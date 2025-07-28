using ArrowDrivingSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArrowDrivingSchool.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<User> Users => Set<User>();


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>()
            .HasMany(d => d.Students)
            .WithOne(s => s.Driver!)
            .HasForeignKey(s => s.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}