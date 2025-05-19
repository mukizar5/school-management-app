using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.SchoolSetting;

public class CreateSchoolSettingDto
{
    [Required]
    public string StudentRegistrationPrefix { get; set; }
}