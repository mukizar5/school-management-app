using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.LessonAttendance;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.LessonAttendances;

namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("lessons/attendances")]  // Updated the route to plural to match conventions
public class LessonAttendancesController : ControllerBase
{
    private readonly ILessonAttendancesService _lessonAttendancesService;
    private readonly IMapper _mapper;

    public LessonAttendancesController(ILessonAttendancesService lessonAttendancesService, IMapper mapper)
    {
        _lessonAttendancesService = lessonAttendancesService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonAttendanceDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LessonAttendanceDto>>> GetAllAsync()
    {
        var lessonAttendances = await _lessonAttendancesService.GetAllAsync();
        var lessonAttendanceDtos = _mapper.Map<IEnumerable<LessonAttendanceDto>>(lessonAttendances);
        return Ok(lessonAttendanceDtos);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonAttendanceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LessonAttendanceDto>> GetLessonAttendanceById(Guid id)
    {
        var lessonAttendance = await _lessonAttendancesService.GetByIdAsync(id);
        if (lessonAttendance == null)
            return NotFound("Lesson attendance not found");
        var lessonAttendanceDto = _mapper.Map<LessonAttendanceDto>(lessonAttendance);
        return Ok(lessonAttendanceDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LessonAttendanceDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LessonAttendanceDto>> AddAsync(CreateLessonAttendanceDto requestObject)
    {
        var newLessonAttendance = _mapper.Map<LessonAttendance>(requestObject);
        var createdLessonAttendance = await  _lessonAttendancesService.InsertAsync(newLessonAttendance);
        var createdLessonAttendanceDto = _mapper.Map<LessonAttendanceDto>(createdLessonAttendance);
        return StatusCode(StatusCodes.Status201Created, createdLessonAttendanceDto);
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateLessonAttendanceDto updatedLessonAttendance)
    {
        if (id != updatedLessonAttendance.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingLessonAttendance = await _lessonAttendancesService.GetByIdAsync(id);
            if (existingLessonAttendance == null)
            {
                return NotFound("Lesson attendance not found");
            }
            _mapper.Map(updatedLessonAttendance, existingLessonAttendance);
            await  _lessonAttendancesService.UpdateAsync(existingLessonAttendance);
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

    // DELETE: students/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _lessonAttendancesService.DeleteAsync(id);
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
