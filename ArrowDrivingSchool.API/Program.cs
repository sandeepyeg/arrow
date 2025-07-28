using ArrowDrivingSchool.Infrastructure.Data;
using ArrowDrivingSchool.Infrastructure.Repositories;
using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using ArrowDrivingSchool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DB context with dynamic path
var dbPath = DbPathHelper.GetDbPath();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Custom services
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();