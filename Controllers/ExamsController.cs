using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SchoolManagementSystem.Api.Dtos.Exam;
using SchoolManagementSystem.Api.Services.Exams;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("exams")]
    public class ExamsController : ControllerBase
    {
        private readonly IExamsService _examsService;
        private readonly IMapper _mapper;

        public ExamsController(IExamsService examsService, IMapper mapper)
        {
            _examsService = examsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetAllAsync([FromQuery] SearchExams searchObject)
        {
            try
            {
                var exams = await _examsService.SearchAsync(searchObject);
                var examsDtos = _mapper.Map<IEnumerable<ExamDto>>(exams);
                return Ok(examsDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExamDto>> GetByIdAsync(Guid id)
        {
            var exam = await _examsService.GetByIdAsync(id);
            if (exam == null)
                return NotFound("Exam not found");
            var examDto = _mapper.Map<ExamDto>(exam);
            return Ok(examDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExamDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExamDto>> AddAsync(CreateExamDto requestObject)
        {
            var newExam = _mapper.Map<Exam>(requestObject);
            var createdExam = await _examsService.InsertAsync(newExam);
            var createdExamDto = _mapper.Map<ExamDto>(createdExam);
            return StatusCode(StatusCodes.Status201Created, createdExamDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateExamDto updatedExam)
        {
            if (id != updatedExam.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingExam = await _examsService.GetByIdAsync(id);
                if (existingExam == null)
                {
                    return NotFound("Exam not found");
                }
                _mapper.Map(updatedExam, existingExam);
                await _examsService.UpdateAsync(existingExam);
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
                await _examsService.DeleteAsync(id);
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
