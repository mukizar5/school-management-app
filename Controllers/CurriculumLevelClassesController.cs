using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.CurriculumLevelClass;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Services.CurriculumLevelClasses;

namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("curriculums/levels/classes")]  // Updated the route to plural to match conventions
public class CurriculumLevelClassesController : ControllerBase
{
    private readonly ICurriculumLevelClassesService _curriculumLevelClassesService;
    private readonly IMapper _mapper;

    public CurriculumLevelClassesController(ICurriculumLevelClassesService curriculumLevelClassesService, IMapper mapper)
    {
        _curriculumLevelClassesService = curriculumLevelClassesService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CurriculumLevelClassDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CurriculumLevelClassDto>>> GetAllAsync()
    {
        var curriculumLevelClasses = await _curriculumLevelClassesService.SearchAsync(new SearchCurriculumLevelClasses());
        return Ok(curriculumLevelClasses);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurriculumLevelClassDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CurriculumLevelClassDto>> GetCurriculumLevelById(Guid id)
    {
        var curriculumLevelClass = await _curriculumLevelClassesService.GetByIdAsync(id);
        if (curriculumLevelClass == null)
            return NotFound("Curriculum level class not found");
        var curriculumLevelClassDto = _mapper.Map<CurriculumLevelClassDto>(curriculumLevelClass);
        return Ok(curriculumLevelClassDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CurriculumLevelClassDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurriculumLevelClassDto>> AddAsync(CreateCurriculumLevelClassDto requestObject)
    {
        var newCurriculumLevelClass = _mapper.Map<CurriculumLevelClass>(requestObject);
        var createdCurriculumLevelClass = await  _curriculumLevelClassesService.InsertAsync(newCurriculumLevelClass);
        var createdCurriculumLevelClassDto = _mapper.Map<CurriculumLevelClassDto>(createdCurriculumLevelClass);
        return StatusCode(StatusCodes.Status201Created, createdCurriculumLevelClassDto);
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateCurriculumLevelClassDto updatedCurriculumLevelClass)
    {
        if (id != updatedCurriculumLevelClass.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingCurriculumLevelClass = await _curriculumLevelClassesService.GetByIdAsync(id);
            if (existingCurriculumLevelClass == null)
            {
                return NotFound("Curriculum level class not found");
            }
            _mapper.Map(updatedCurriculumLevelClass, existingCurriculumLevelClass);
            await  _curriculumLevelClassesService.UpdateAsync(existingCurriculumLevelClass);
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
            await _curriculumLevelClassesService.DeleteAsync(id);
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
