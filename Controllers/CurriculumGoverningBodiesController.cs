using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Dtos.CurriculumGoverningBody;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.Services.CurriculumGoverningBodies;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("curriculums/governing-bodies")]
    public class CurriculumGoverningBodiesController : ControllerBase
    {
        private readonly ICurriculumGoverningBodiesService _curriculumGoverningBodiesService;

        public CurriculumGoverningBodiesController(ICurriculumGoverningBodiesService curriculumGoverningBodiesService)
        {
            _curriculumGoverningBodiesService= curriculumGoverningBodiesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CurriculumGoverningBodyDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CurriculumGoverningBodyDto>>> GetAllAsync()
        {
            var curriculumGoverningBodies = await _curriculumGoverningBodiesService.GetAllAsync();
            var curriculumGoverningBodiesDto = curriculumGoverningBodies.Select(cg => new CurriculumGoverningBodyDto
            {
                Id = cg.Id,
                FullName = cg.FullName,
                Abbreviation = cg.Abbreviation,
                Description = cg.Description
            });
            return Ok(curriculumGoverningBodiesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurriculumGoverningBodyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurriculumGoverningBodyDto>> GetByIdAsync(Guid id)
        {
            var curriculumGoverningBody = await _curriculumGoverningBodiesService.GetByIdAsync(id);
            if (curriculumGoverningBody == null)
                return NotFound("Curriculum Governing Body not found");
            var curriculumGoverningBodyDto = new CurriculumGoverningBodyDto
            {
                Id = curriculumGoverningBody.Id,
                FullName = curriculumGoverningBody.FullName,
                Abbreviation = curriculumGoverningBody.Abbreviation,
                Description = curriculumGoverningBody.Description
            };
            return Ok(curriculumGoverningBodyDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CurriculumGoverningBodyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CurriculumGoverningBodyDto>> AddAsync(CreateCurriculumGoverningBodyDto requestObject)
        {
            var newCurriculumGoverningBody = new CurriculumGoverningBody
            {
                FullName = requestObject.FullName,
                Abbreviation = requestObject.Abbreviation,
                Description = requestObject.Description
            };
            var createdCurriculumGoverningBody = await _curriculumGoverningBodiesService.InsertAsync(newCurriculumGoverningBody);
            var createdCurriculumGoverningBodyDto = new CurriculumGoverningBodyDto
            {
                Id = createdCurriculumGoverningBody.Id,
                FullName = createdCurriculumGoverningBody.FullName,
                Abbreviation = createdCurriculumGoverningBody.Abbreviation,
                Description = createdCurriculumGoverningBody.Description
            };
            return StatusCode(StatusCodes.Status201Created, createdCurriculumGoverningBodyDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateCurriculumGoverningBodyDto updatedCurriculumGoverningBody)
        {
            if (id != updatedCurriculumGoverningBody.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingCurriculumGoverningBody = await _curriculumGoverningBodiesService.GetByIdAsync(id);
                if (existingCurriculumGoverningBody == null)
                {
                    return NotFound("Curriculum Governing Body not found");
                }
                existingCurriculumGoverningBody.FullName = updatedCurriculumGoverningBody.FullName;
                existingCurriculumGoverningBody.Abbreviation = updatedCurriculumGoverningBody.Abbreviation;
                existingCurriculumGoverningBody.Description = updatedCurriculumGoverningBody.Description;
                await _curriculumGoverningBodiesService.UpdateAsync(existingCurriculumGoverningBody);
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
                await _curriculumGoverningBodiesService.DeleteAsync(id);
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
