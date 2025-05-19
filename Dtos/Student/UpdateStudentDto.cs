using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Dtos.Student;

public record UpdateStudentDto:CreateStudentDto
{
    [Required]
    public Guid Id { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime? ExitDate { get; set; }
}