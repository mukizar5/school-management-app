using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.AssignmentResult;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.AssignmentResults;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("assignments/results")]
    public class AssignmentResultsController : ControllerBase
    {
        private readonly IAssignmentResultsService _assignmentResultsService;
        private readonly IMapper _mapper;

        public AssignmentResultsController(IAssignmentResultsService assignmentResultsService, IMapper mapper)
        {
            _assignmentResultsService= assignmentResultsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AssignmentResultDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AssignmentResultDto>>> GetAllAsync()
        {
            var assignmentResults = await _assignmentResultsService.GetAllAsync();
            var assignmentResultDtos = _mapper.Map<IEnumerable<AssignmentResultDto>>(assignmentResults);
            return Ok(assignmentResultDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentResultDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AssignmentResultDto>> GetAssignmentResultById(Guid id)
        {
            var assignmentResult = await _assignmentResultsService.GetByIdAsync(id);
            if (assignmentResult == null)
                return NotFound("Assignment result not found");
            var assignmentResultDto = _mapper.Map<AssignmentResultDto>(assignmentResult);
            return Ok(assignmentResultDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AssignmentResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AssignmentResultDto>> AddAsync(CreateAssignmentResultDto requestObject)
        {
            var newAssignmentResult = _mapper.Map<AssignmentResult>(requestObject);
            var createdAssignmentResult = await _assignmentResultsService.InsertAsync(newAssignmentResult);
            var createdAssignmentResultDto = _mapper.Map<AssignmentResultDto>(createdAssignmentResult);
            return StatusCode(StatusCodes.Status201Created, createdAssignmentResultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateAssignmentResultDto updatedAssignmentResult)
        {
            if (id != updatedAssignmentResult.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingAssignmentResult = await _assignmentResultsService.GetByIdAsync(id);
                if (existingAssignmentResult == null)
                {
                    return NotFound("Assignment result not found");
                }
                _mapper.Map(updatedAssignmentResult, existingAssignmentResult);
                await _assignmentResultsService.UpdateAsync(existingAssignmentResult);
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
                await _assignmentResultsService.DeleteAsync(id);
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
