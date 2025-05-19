using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Api.Dtos.SchoolSetting;

public class UpdateSchoolSettingDto : CreateSchoolSettingDto
{
    [Required]
    public Guid Id { get; set; }
}