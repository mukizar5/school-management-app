namespace SchoolManagementSystem.Api.Models;

public class ClassStream : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Student>? Students { get; set; }
}