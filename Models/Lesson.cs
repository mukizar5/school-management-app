namespace SchoolManagementSystem.Api.Models;

public class Lesson: BaseModel
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public Guid CurriculumLevelClassId { get; set; }
    public CurriculumLevelClass? CurriculumLevelClass { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
}