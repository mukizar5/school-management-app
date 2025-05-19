using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumLevel;

public class UpdateCurriculumLevelDto : CreateCurriculumLevelDto
{
    [Required]
    public Guid Id { get; set; }
}
