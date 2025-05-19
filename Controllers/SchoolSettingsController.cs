using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.SchoolSetting;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.SchoolSettings;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("school-settings")]
    public class SchoolSettingsController : ControllerBase
    {
        private readonly ISchoolSettingsService _schoolSettingsService;
        private readonly IMapper _mapper;

        public SchoolSettingsController(ISchoolSettingsService schoolSettingsService, IMapper mapper)
        {
            _schoolSettingsService = schoolSettingsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SchoolSettingDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SchoolSettingDto>>> GetAllAsync()
        {
            var schoolSettings = await _schoolSettingsService.GetAllAsync();
            var schoolSettingDtos = _mapper.Map<IEnumerable<SchoolSettingDto>>(schoolSettings);
            return Ok(schoolSettingDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchoolSettingDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SchoolSettingDto>> GetSchoolSettingById(Guid id)
        {
            var schoolSetting = await _schoolSettingsService.GetByIdAsync(id);
            if (schoolSetting == null)
                return NotFound("School setting not found");
            var schoolSettingDto = _mapper.Map<SchoolSettingDto>(schoolSetting);
            return Ok(schoolSettingDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SchoolSettingDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SchoolSettingDto>> AddAsync(CreateSchoolSettingDto requestObject)
        {
            var newSchoolSetting = _mapper.Map<SchoolSetting>(requestObject);
            var createdSchoolSetting = await _schoolSettingsService.InsertAsync(newSchoolSetting);
            var createdSchoolSettingDto = _mapper.Map<SchoolSettingDto>(createdSchoolSetting);
            return StatusCode(StatusCodes.Status201Created, createdSchoolSettingDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateSchoolSettingDto updatedSchoolSetting)
        {
            if (id != updatedSchoolSetting.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingSchoolSetting = await _schoolSettingsService.GetByIdAsync(id);
                if (existingSchoolSetting == null)
                {
                    return NotFound("School setting not found");
                }
                _mapper.Map(updatedSchoolSetting, existingSchoolSetting);
                await _schoolSettingsService.UpdateAsync(existingSchoolSetting);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _schoolSettingsService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
