using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Dtos.Teacher;

public record TeacherDto
{
    [Required]
    public Guid Id {get; set;}
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public DateOnly? DateOfBirth { get; set; }
    public DateTime EmploymentDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string Status { get; set; }
    [Required]
    public string MarriageStatus { get; set; }
    [Required]
    public int Salary { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    public List<NameIdPair>? Subjects { get; set; }
    public List<NameIdPair>? Classes { get; set; }
}