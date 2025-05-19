using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.CocurricularActivity;

public class UpdateCocurricularActivityDto: CreateCocurricularActivityDto
{
    [Required]
    public Guid Id { get; set; }
}