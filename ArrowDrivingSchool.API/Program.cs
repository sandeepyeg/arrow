using System.Text;
using ArrowDrivingSchool.API.Middleware;
using ArrowDrivingSchool.Domain.Entities;
using ArrowDrivingSchool.Infrastructure.Data;
using ArrowDrivingSchool.Infrastructure.Repositories;
using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using ArrowDrivingSchool.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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


//logging


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


// Replace built-in logging with Serilog
builder.Host.UseSerilog();

var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();
// Auth Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.Add(new User
        {
            Email = "admin@arrow.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("@dmoanton!50th")
        });
        db.SaveChanges();
    }
}

// Middleware for logging requests
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseAuthentication(); // before Authorization
app.UseAuthorization();

// Backup database on application exit
AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    var dbPath = DbPathHelper.GetDbPath();
    BackupHelper.BackupDatabase(dbPath);
};


app.Run();