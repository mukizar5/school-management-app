namespace SchoolManagementSystem.Api.Models;

public class ExamType : BaseModel
{
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}
