namespace SchoolManagementSystem.Api.Models;

public class CurriculumGoverningBody : BaseModel
{
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
}