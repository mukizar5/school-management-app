using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.ExamType;
using SchoolManagementSystem.Api.Services.ExamTypes;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("exams/types")]
    public class ExamTypesController : ControllerBase
    {
        private readonly IExamTypesService _examTypesService;

        public ExamTypesController(IExamTypesService examTypesService)
        {
            _examTypesService = examTypesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamTypeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ExamTypeDto>>> GetAllAsync()
        {
            var examTypes = await _examTypesService.GetAllAsync();
            var examTypesDto = examTypes.Select(et => new ExamTypeDto
            {
                Id = et.Id,
                FullName = et.FullName,
                Abbreviation = et.Abbreviation,
                Description = et.Description
            });
            return Ok(examTypesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExamTypeDto>> GetByIdAsync(Guid id)
        {
            var examType = await _examTypesService.GetByIdAsync(id);
            if (examType == null)
                return NotFound("Exam Type not found");
            var examTypeDto = new ExamTypeDto
            {
                Id = examType.Id,
                FullName = examType.FullName,
                Abbreviation = examType.Abbreviation,
                Description = examType.Description
            };
            return Ok(examTypeDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExamTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExamTypeDto>> AddAsync(CreateExamTypeDto requestObject)
        {
            var newExamType = new ExamType
            {
                FullName = requestObject.FullName,
                Abbreviation = requestObject.Abbreviation,
                Description = requestObject.Description
            };
            var createdExamType = await _examTypesService.InsertAsync(newExamType);
            var createdExamTypeDto = new ExamTypeDto
            {
                Id = createdExamType.Id,
                FullName = createdExamType.FullName,
                Abbreviation = createdExamType.Abbreviation,
                Description = createdExamType.Description
            };
            return StatusCode(StatusCodes.Status201Created, createdExamTypeDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, ExamTypeDto updatedExamType)
        {
            if (id != updatedExamType.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingExamType = await _examTypesService.GetByIdAsync(id);
                if (existingExamType == null)
                {
                    return NotFound("Exam Type not found");
                }
                existingExamType.FullName = updatedExamType.FullName;
                existingExamType.Abbreviation = updatedExamType.Abbreviation;
                existingExamType.Description = updatedExamType.Description;
                await _examTypesService.UpdateAsync(existingExamType);
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
                await _examTypesService.DeleteAsync(id);
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
