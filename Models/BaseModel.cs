namespace SchoolManagementSystem.Api.Models;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    public Guid? CreatedById { get; set; }
    public Guid? LastUpdatedById { get; set; }
    public bool IsDeleted { get; set; }
}