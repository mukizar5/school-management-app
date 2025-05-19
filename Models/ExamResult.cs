using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Models;

public class ExamResult : BaseModel
{
    [Required]
    public Guid StudentId { get; set; }
    public int Results { get; set; }
    [Required]
    public Guid ExamId { get; set; }
    
    
}

