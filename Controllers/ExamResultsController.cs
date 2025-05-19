using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.ExamResult;
using SchoolManagementSystem.Api.Services.ExamResults;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("exams/results")]
    public class ExamResultsController : ControllerBase
    {
        private readonly IExamResultsService _examResultsService;

        public ExamResultsController(IExamResultsService examResultsService)
        {
            _examResultsService = examResultsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamResultDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ExamResultDto>>> GetAllAsync()
        {
            var examResults = await _examResultsService.GetAllAsync();
            var examResultsDto = examResults.Select(e => new ExamResultDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                Results = e.Results,
                ExamId = e.ExamId,
            });
            return Ok(examResultsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamResultDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExamResultDto>> GetByIdAsync(Guid id)
        {
            var examResult = await _examResultsService.GetByIdAsync(id);
            if (examResult == null)
                return NotFound("ExamResult not found");
            var examResultDto = new ExamResultDto
            {
                 Id = examResult.Id,
                 StudentId = examResult.StudentId,
                 Results = examResult.Results,
                 ExamId = examResult.ExamId,
                
            };
            return Ok(examResultDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExamResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExamResultDto>> AddAsync(CreateExamResultDto requestObject)
        {
            var newExamResult = new ExamResult
            {
                
                StudentId = requestObject.StudentId,
                Results = requestObject.Results,
                ExamId = requestObject.ExamId,
            };
            var createdExamResult = await _examResultsService.InsertAsync(newExamResult);
            var createdExamResultDto = new ExamResultDto
            {
                 Id = createdExamResult.Id,
                 StudentId = createdExamResult.StudentId,
                 Results = createdExamResult.Results,
                 ExamId = createdExamResult.ExamId,
                
            };
            return StatusCode(StatusCodes.Status201Created, createdExamResultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, ExamResultDto updatedExamResult)
        {
            if (id != updatedExamResult.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingExamResult = await _examResultsService.GetByIdAsync(id);
                if (existingExamResult == null)
                {
                    return NotFound("ExamResult not found");
                }
                
                existingExamResult.StudentId = updatedExamResult.StudentId;
                existingExamResult.Results = updatedExamResult.Results;
                existingExamResult.ExamId = updatedExamResult.ExamId;
                await _examResultsService.UpdateAsync(existingExamResult);
                
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
                await _examResultsService.DeleteAsync(id);
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
