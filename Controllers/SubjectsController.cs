using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.Subject;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.Subjects;
using SchoolManagementSystem.Api.SearchObjects;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectsService subjectsService, IMapper mapper)
        {
            _subjectsService= subjectsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubjectDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAllAsync([FromQuery] SearchSubjects searchObject)
        {
            try
            {
                var subjects = await _subjectsService.SearchAsync(searchObject);
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(Guid id)
        {
            var subject = await _subjectsService.GetByIdAsync(id);
            if (subject == null)
                return NotFound("Subject not found");
            var subjectDto = _mapper.Map<SubjectDto>(subject);
            return Ok(subjectDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubjectDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubjectDto>> AddAsync(CreateSubjectDto requestObject)
        {
            var newSubject = _mapper.Map<Subject>(requestObject);
            var createdSubject = await _subjectsService.InsertAsync(newSubject);
            var createdSubjectDto = _mapper.Map<SubjectDto>(createdSubject);
            return StatusCode(StatusCodes.Status201Created, createdSubjectDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateSubjectDto updatedSubject)
        {
            if (id != updatedSubject.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingSubject = await _subjectsService.GetByIdAsync(id);
                if (existingSubject == null)
                {
                    return NotFound("Subject not found");
                }
                _mapper.Map(updatedSubject, existingSubject);
                await _subjectsService.UpdateAsync(existingSubject);
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
                await _subjectsService.DeleteAsync(id);
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
