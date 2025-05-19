using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.CurriculumLevel;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.CurriculumLevels;
using SchoolManagementSystem.Api.SearchObjects;
namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("curriculums/levels")]  // Updated the route to plural to match conventions
public class CurriculumLevelsController : ControllerBase
{
    private readonly ICurriculumLevelsService _curriculumLevelsService;
    private readonly IMapper _mapper;

    public CurriculumLevelsController(ICurriculumLevelsService curriculumLevelsService, IMapper mapper)
    {
        _curriculumLevelsService = curriculumLevelsService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CurriculumLevelDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CurriculumLevelDto>>> GetAllAsync()
    {
        var curriculumLevels = await _curriculumLevelsService.SearchAsync(new SearchCurriculumLevels());
        return Ok(curriculumLevels);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurriculumLevelDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CurriculumLevelDto>> GetCurriculumLevelById(Guid id)
    {
        var curriculumLevel = await _curriculumLevelsService.GetByIdAsync(id);
        if (curriculumLevel == null)
            return NotFound("Curriculum level not found");
        var curriculumLevelDto = _mapper.Map<CurriculumLevelDto>(curriculumLevel);
        return Ok(curriculumLevelDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CurriculumLevelDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurriculumLevelDto>> AddAsync(CreateCurriculumLevelDto requestObject)
    {
        var newCurriculumLevel = _mapper.Map<CurriculumLevel>(requestObject);
        var createdCurriculumLevel = await  _curriculumLevelsService.InsertAsync(newCurriculumLevel);
        var createdCurriculumLevelDto = _mapper.Map<CurriculumLevelDto>(createdCurriculumLevel);
        return StatusCode(StatusCodes.Status201Created, createdCurriculumLevelDto);
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateCurriculumLevelDto updatedCurriculumLevel)
    {
        if (id != updatedCurriculumLevel.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingCurriculumLevel = await _curriculumLevelsService.GetByIdAsync(id);
            if (existingCurriculumLevel == null)
            {
                return NotFound("Curriculum level not found");
            }
            _mapper.Map(updatedCurriculumLevel, existingCurriculumLevel);
            await  _curriculumLevelsService.UpdateAsync(existingCurriculumLevel);
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
            await _curriculumLevelsService.DeleteAsync(id);
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
