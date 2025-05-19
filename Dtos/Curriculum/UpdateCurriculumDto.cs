using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.Curriculum;

public class UpdateCurriculumDto : CreateCurriculumDto
{
    [Required]
    public Guid Id { get; set; }
}
