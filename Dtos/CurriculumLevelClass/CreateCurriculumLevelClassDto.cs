using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CurriculumLevelClass;

public class CreateCurriculumLevelClassDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public Guid CurriculumLevelId { get; set; }
}
