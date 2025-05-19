using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Dtos.Teacher;

public record UpdateTeacherDto : CreateTeacherDto
{
    [Required]
    public Guid Id { get; set; }
    public DateTime? ExitDate { get; set; }
    public string Status { get; set; } = "Active";
}