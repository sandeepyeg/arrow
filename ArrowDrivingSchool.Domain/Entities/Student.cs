namespace ArrowDrivingSchool.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string UniqueId { get; set; } = string.Empty; 
    public string FullName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public int TotalHours { get; set; }
    public bool CertificateIssued { get; set; }
    public int DriverId { get; set; }

    public Driver? Driver { get; set; }
}