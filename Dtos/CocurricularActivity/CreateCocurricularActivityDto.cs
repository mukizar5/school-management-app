using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CocurricularActivity;

public class CreateCocurricularActivityDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}