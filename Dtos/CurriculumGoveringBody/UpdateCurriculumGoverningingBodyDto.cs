using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumGoverningBody;

public record UpdateCurriculumGoverningBodyDto:CreateCurriculumGoverningBodyDto
{
    [Required]
    public Guid Id { get; set; }
}