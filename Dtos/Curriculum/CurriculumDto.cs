namespace SchoolManagementSystem.Api.Dtos.Curriculum;

public class CurriculumDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public NameIdPair CurriculumGoverningBody { get; set; }
}
