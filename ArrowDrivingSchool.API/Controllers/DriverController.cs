using ArrowDrivingSchool.Infrastructure.Repositories.Interfaces;
using ArrowDrivingSchool.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ArrowDrivingSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController(IDriverRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DriverDto>>> Get()
    {
        var drivers = await repo.GetAllAsync();
        return Ok(drivers.Select(d => new DriverDto
        {
            Id = d.Id,
            FullName = d.FullName,
            Phone = d.Phone,
            IsActive = d.IsActive
        }));
    }

    [HttpPost]
    public async Task<ActionResult> Post(DriverDto dto)
    {
        await repo.AddAsync(new Domain.Entities.Driver
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            IsActive = dto.IsActive
        });
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, DriverDto dto)
    {
        var driver = await repo.GetByIdAsync(id);
        if (driver == null) return NotFound();

        driver.FullName = dto.FullName;
        driver.Phone = dto.Phone;
        driver.IsActive = dto.IsActive;

        await repo.UpdateAsync(driver);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await repo.DeleteAsync(id);
        return Ok();
    }
}