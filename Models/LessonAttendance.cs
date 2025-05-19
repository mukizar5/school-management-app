namespace SchoolManagementSystem.Api.Models;

public class LessonAttendance: BaseModel
{
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public bool IsPresent { get; set; }
}
