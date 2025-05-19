using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumLevelClass;

public class UpdateCurriculumLevelClassDto : CreateCurriculumLevelClassDto
{
    [Required]
    public Guid Id { get; set; }
}
