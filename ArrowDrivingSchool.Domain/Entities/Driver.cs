namespace ArrowDrivingSchool.Domain.Entities;

public class Driver
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}