using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.CocurricularActivity;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.CocurricularActivities;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("cocurricular-activities")]
    public class CocurricularActivitiesController : ControllerBase
    {
        private readonly ICocurricularActivitiesService _cocurricularActivitiesService;
        private readonly IMapper _mapper;

        public CocurricularActivitiesController(ICocurricularActivitiesService cocurricularActivitiesService, IMapper mapper)
        {
            _cocurricularActivitiesService= cocurricularActivitiesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CocurricularActivityDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CocurricularActivityDto>>> GetAllAsync()
        {
            var cocurricularActivities = await _cocurricularActivitiesService.GetAllAsync();
            var cocurricularActivityDtos = _mapper.Map<IEnumerable<CocurricularActivityDto>>(cocurricularActivities);
            return Ok(cocurricularActivityDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CocurricularActivityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CocurricularActivityDto>> GetByIdAsync(Guid id)
        {
            var cocurricularActivity = await _cocurricularActivitiesService.GetByIdAsync(id);
            if (cocurricularActivity == null)
                return NotFound("Cocurricular activity not found");
            var cocurricularActivityDto = _mapper.Map<CocurricularActivityDto>(cocurricularActivity);
            return Ok(cocurricularActivityDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CocurricularActivityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CocurricularActivityDto>> AddAsync(CreateCocurricularActivityDto requestObject)
        {
            var newCocurricularActivity = _mapper.Map<CocurricularActivity>(requestObject);
            var createdCocurricularActivity = await _cocurricularActivitiesService.InsertAsync(newCocurricularActivity);
            var createdCocurricularActivityDto = _mapper.Map<CocurricularActivityDto>(createdCocurricularActivity);
            return StatusCode(StatusCodes.Status201Created, createdCocurricularActivityDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateCocurricularActivityDto updatedCocurricularActivity)
        {
            if (id != updatedCocurricularActivity.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingCocurricularActivity = await _cocurricularActivitiesService.GetByIdAsync(id);
                if (existingCocurricularActivity == null)
                {
                    return NotFound("Cocurricular activity not found");
                }
                _mapper.Map(updatedCocurricularActivity, existingCocurricularActivity);
                await _cocurricularActivitiesService.UpdateAsync(existingCocurricularActivity);
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
                await _cocurricularActivitiesService.DeleteAsync(id);
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
