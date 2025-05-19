using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.Lesson;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.Lessons;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("lessons")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsService _lessonsService;
        private readonly IMapper _mapper;

        public LessonsController(ILessonsService lessonsService, IMapper mapper)
        {
            _lessonsService= lessonsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllAsync()
        {
            var lessons = await _lessonsService.GetAllAsync();
            var lessonDtos = _mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(lessonDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LessonDto>> GetLessonById(Guid id)
        {
            var lesson = await _lessonsService.GetByIdAsync(id);
            if (lesson == null)
                return NotFound("Lesson not found");
            var lessonDto = _mapper.Map<LessonDto>(lesson);
            return Ok(lessonDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LessonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LessonDto>> AddAsync(CreateLessonDto requestObject)
        {
            var newLesson = _mapper.Map<Lesson>(requestObject);
            var createdLesson = await _lessonsService.InsertAsync(newLesson);
            var createdLessonDto = _mapper.Map<LessonDto>(createdLesson);
            return StatusCode(StatusCodes.Status201Created, createdLessonDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateLessonDto updatedLesson)
        {
            if (id != updatedLesson.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingLesson = await _lessonsService.GetByIdAsync(id);
                if (existingLesson == null)
                {
                    return NotFound("Class stream not found");
                }
                _mapper.Map(updatedLesson, existingLesson);
                await _lessonsService.UpdateAsync(existingLesson);
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
                await _lessonsService.DeleteAsync(id);
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
