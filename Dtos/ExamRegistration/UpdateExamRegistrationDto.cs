using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.ExamRegistration;

public record UpdateExamRegistrationDto:CreateExamRegistrationDto
{
    [Required]
    public Guid Id { get; set; }
}