namespace SchoolManagementSystem.Api.Dtos.Guardian;

public class GuardianDto: BaseProfileDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
}