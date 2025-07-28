using ArrowDrivingSchool.Application.Utils;
using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using ArrowDrivingSchool.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArrowDrivingSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class StudentController(IStudentRepository repo, IDriverRepository driverRepo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentDto>>> Get([FromQuery] string? search)
    {
        var students = await repo.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            students = students.Where(s =>
                s.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                s.Phone.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                s.UniqueId.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return Ok(students.Select(s => new StudentDto
        {
            Id = s.Id,
            UniqueId = s.UniqueId,
            FullName = s.FullName,
            Gender = s.Gender,
            Address = s.Address,
            Phone = s.Phone,
            Email = s.Email,
            TotalAmount = s.TotalAmount,
            AmountPaid = s.AmountPaid,
            TotalHours = s.TotalHours,
            CertificateIssued = s.CertificateIssued,
            DriverId = s.DriverId,
            DriverName = s.Driver?.FullName
        }));
    }

    [HttpPost]
    public async Task<ActionResult> Post(StudentDto dto)
    {
        var student = new Domain.Entities.Student
        {
            UniqueId = UniqueIdGenerator.Generate(),
            FullName = dto.FullName,
            Gender = dto.Gender,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email,
            TotalAmount = dto.TotalAmount,
            AmountPaid = dto.AmountPaid,
            TotalHours = dto.TotalHours,
            CertificateIssued = dto.CertificateIssued,
            DriverId = dto.DriverId
        };

        await repo.AddAsync(student);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, StudentDto dto)
    {
        var student = await repo.GetByIdAsync(id);
        if (student == null) return NotFound();

        student.FullName = dto.FullName;
        student.Gender = dto.Gender;
        student.Address = dto.Address;
        student.Phone = dto.Phone;
        student.Email = dto.Email;
        student.TotalAmount = dto.TotalAmount;
        student.AmountPaid = dto.AmountPaid;
        student.TotalHours = dto.TotalHours;
        student.CertificateIssued = dto.CertificateIssued;
        student.DriverId = dto.DriverId;

        await repo.UpdateAsync(student);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await repo.DeleteAsync(id);
        return Ok();
    }
}