using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.Assignment;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.Assignments;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("assignments")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentsService _assignmentsService;
        private readonly IMapper _mapper;

        public AssignmentsController(IAssignmentsService assignmentsService, IMapper mapper)
        {
            _assignmentsService= assignmentsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AssignmentDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAllAsync()
        {
            var assignments = await _assignmentsService.GetAllAsync();
            var assignmentDtos = _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
            return Ok(assignmentDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AssignmentDto>> GetAssignmentById(Guid id)
        {
            var assignment = await _assignmentsService.GetByIdAsync(id);
            if (assignment == null)
                return NotFound("Assignment not found");
            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return Ok(assignmentDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AssignmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AssignmentDto>> AddAsync(CreateAssignmentDto requestObject)
        {
            var newAssignment = _mapper.Map<Assignment>(requestObject);
            var createdAssignment = await _assignmentsService.InsertAsync(newAssignment);
            var createdAssignmentDto = _mapper.Map<AssignmentDto>(createdAssignment);
            return StatusCode(StatusCodes.Status201Created, createdAssignmentDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateAssignmentDto updatedAssignment)
        {
            if (id != updatedAssignment.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingAssignment = await _assignmentsService.GetByIdAsync(id);
                if (existingAssignment == null)
                {
                    return NotFound("Class stream not found");
                }
                _mapper.Map(updatedAssignment, existingAssignment);
                await _assignmentsService.UpdateAsync(existingAssignment);
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
                await _assignmentsService.DeleteAsync(id);
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
