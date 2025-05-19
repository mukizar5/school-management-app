using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.Curriculum;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Services.Curriculums;

namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("curriculums")]  // Updated the route to plural to match conventions
public class CurriculumsController : ControllerBase
{
    private readonly ICurriculumsService _curriculumsService;
    private readonly IMapper _mapper;

    public CurriculumsController(ICurriculumsService curriculumsService, IMapper mapper)
    {
        _curriculumsService = curriculumsService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CurriculumDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CurriculumDto>>> GetAllAsync()
    {
        var curriculums = await _curriculumsService.SearchAsync(new SearchCurriculums());
        return Ok(curriculums);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurriculumDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CurriculumDto>> GetCurriculumById(Guid id)
    {
        var curriculum = await _curriculumsService.GetByIdAsync(id);
        if (curriculum == null)
            return NotFound("Curriculum not found");
        var curriculumDto = _mapper.Map<CurriculumDto>(curriculum);
        return Ok(curriculumDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CurriculumDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurriculumDto>> AddAsync(CreateCurriculumDto requestObject)
    {
        var newCurriculum = _mapper.Map<Curriculum>(requestObject);
        var createdCurriculum = await  _curriculumsService.InsertAsync(newCurriculum);
        var createdCurriculumDto = _mapper.Map<CurriculumDto>(createdCurriculum);
        return StatusCode(StatusCodes.Status201Created, createdCurriculumDto);
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateCurriculumDto updatedCurriculum)
    {
        if (id != updatedCurriculum.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingCurriculum = await _curriculumsService.GetByIdAsync(id);
            if (existingCurriculum == null)
            {
                return NotFound("Curriculum not found");
            }
            _mapper.Map(updatedCurriculum, existingCurriculum);
            await  _curriculumsService.UpdateAsync(existingCurriculum);
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
            await _curriculumsService.DeleteAsync(id);
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
