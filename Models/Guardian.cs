using SchoolManagementSystem.Api.Enums;

namespace SchoolManagementSystem.Api.Models;

public class Guardian: BaseProfile
{
    public Status Status { get; set; } = Status.Active;
    public ICollection<Student> Students { get; set; }
}