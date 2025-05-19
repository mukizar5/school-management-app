using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Dtos.Teacher;

public record CreateTeacherDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [RegularExpression("(Male|Female)", ErrorMessage = "Gender must be either 'Male' or 'Female'")]
    public string Gender { get; set; }
    [Required(ErrorMessage = "Date of Birth is required")]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public string MarriageStatus { get; set; }
    [Required]
    public int Salary { get; set; }
    public DateTime EmploymentDate { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    public List<Guid>? SubjectIds { get; set; }
    public List<Guid>? ClassIds { get; set; }
}