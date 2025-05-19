using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.ClassStream;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.ClassStreams;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("classes/streams")]
    public class ClassStreamsController : ControllerBase
    {
        private readonly IClassStreamsService _classStreamsService;
        private readonly IMapper _mapper;

        public ClassStreamsController(IClassStreamsService classStreamsService, IMapper mapper)
        {
            _classStreamsService= classStreamsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassStreamDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClassStreamDto>>> GetAllAsync()
        {
            var classStreams = await _classStreamsService.GetAllAsync();
            var classStreamDtos = _mapper.Map<IEnumerable<ClassStreamDto>>(classStreams);
            return Ok(classStreamDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassStreamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassStreamDto>> GetClassStreamById(Guid id)
        {
            var classStream = await _classStreamsService.GetByIdAsync(id);
            if (classStream == null)
                return NotFound("Class stream not found");
            var classStreamDto = _mapper.Map<ClassStreamDto>(classStream);
            return Ok(classStreamDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClassStreamDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClassStreamDto>> AddAsync(CreateClassStreamDto requestObject)
        {
            var newClassStream = _mapper.Map<ClassStream>(requestObject);
            var createdClassStream = await _classStreamsService.InsertAsync(newClassStream);
            var createdClassStreamDto = _mapper.Map<ClassStreamDto>(createdClassStream);
            return StatusCode(StatusCodes.Status201Created, createdClassStreamDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateClassStreamDto updatedClassStream)
        {
            if (id != updatedClassStream.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingClassStream = await _classStreamsService.GetByIdAsync(id);
                if (existingClassStream == null)
                {
                    return NotFound("Class stream not found");
                }
                _mapper.Map(updatedClassStream, existingClassStream);
                await _classStreamsService.UpdateAsync(existingClassStream);
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
                await _classStreamsService.DeleteAsync(id);
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
