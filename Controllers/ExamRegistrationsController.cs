using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.ExamRegistration; // Updated to use the correct DTO
using SchoolManagementSystem.Api.Services.ExamRegistrations; // Updated to use the correct service
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("exams/registrations")] // Updated route to reflect the correct resource
    public class ExamRegistrationsController : ControllerBase // Updated class name
    {
        private readonly IExamRegistrationsService _examRegistrationService; // Updated service interface

        public ExamRegistrationsController(IExamRegistrationsService examRegistrationService) // Updated constructor
        {
            _examRegistrationService = examRegistrationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamRegistrationDto>))] // Updated DTO type
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ExamRegistrationDto>>> GetAllAsync() // Updated return type
        {
            var examRegistrations = await _examRegistrationService.GetAllAsync(); // Updated service call
            var examRegistrationsDto = examRegistrations.Select(e => new ExamRegistrationDto // Updated DTO mapping
            {
                // examRegistrationMapping
                Id = e.Id,
                StudentId = e.StudentId,
                ExamId = e.ExamId,
                ExamType = e.ExamType,
                HasDone = e.HasDone,
                Comment = e.Comment,
            });
            return Ok(examRegistrationsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistrationDto))] // Updated DTO type
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExamRegistrationDto>> GetByIdAsync(Guid id) // Updated return type
        {
            var examRegistration = await _examRegistrationService.GetByIdAsync(id); // Updated service call
            if (examRegistration == null)
                return NotFound("Exam registration not found"); // Updated message
            var examRegistrationDto = new ExamRegistrationDto // Updated DTO mapping
            {
                Id = examRegistration.Id,
                StudentId = examRegistration.StudentId,
                ExamId = examRegistration.ExamId,
                ExamType = examRegistration.ExamType,
                HasDone = examRegistration.HasDone,
                Comment = examRegistration.Comment, // Added missing property
            };
            return Ok(examRegistrationDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExamRegistrationDto))] // Updated DTO type
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExamRegistrationDto>> AddAsync(CreateExamRegistrationDto requestObject) // Updated DTO type
        {
            var newExamRegistration = new ExamRegistration // Updated model
            {
                StudentId = requestObject.StudentId,
                ExamId = requestObject.ExamId,
                ExamType = requestObject.ExamType,
                HasDone = requestObject.HasDone,
                Comment = requestObject.Comment, // Added missing property
            };
            var createdExamRegistration = await _examRegistrationService.InsertAsync(newExamRegistration); // Updated service call
            var createdExamRegistrationDto = new ExamRegistrationDto // Updated DTO mapping
            {
                Id = createdExamRegistration.Id,
                StudentId = createdExamRegistration.StudentId,
                ExamId = createdExamRegistration.ExamId,
                ExamType = createdExamRegistration.ExamType,
                HasDone = createdExamRegistration.HasDone,
                Comment = createdExamRegistration.Comment, // Added missing property
            };
            return StatusCode(StatusCodes.Status201Created, createdExamRegistrationDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, ExamRegistrationDto updatedExamRegistration) // Updated DTO type
        {
            if (id != updatedExamRegistration.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingExamRegistration = await _examRegistrationService.GetByIdAsync(id); // Updated service call
                if (existingExamRegistration == null)
                {
                    return NotFound("Exam registration not found"); // Updated message
                }
                
                existingExamRegistration.StudentId = updatedExamRegistration.StudentId; // Updated properties
                existingExamRegistration.ExamId = updatedExamRegistration.ExamId;
                existingExamRegistration.ExamType = updatedExamRegistration.ExamType;
                existingExamRegistration.HasDone = updatedExamRegistration.HasDone;
                existingExamRegistration.Comment = updatedExamRegistration.Comment; // Added missing property
                await _examRegistrationService.UpdateAsync(existingExamRegistration); // Updated service call
                
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
                await _examRegistrationService.DeleteAsync(id); // Updated service call
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
