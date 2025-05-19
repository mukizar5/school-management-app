using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SchoolManagementSystem.Api.Dtos;
using SchoolManagementSystem.Api.Dtos.Guardian;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Services.Guardians;

namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("guardians")]  // Updated the route to plural to match conventions
public class GuardiansController : ControllerBase
{
    private readonly IGuardiansService _guardiansService;
    private readonly IMapper _mapper;

    public GuardiansController(IGuardiansService guardiansService, IMapper mapper)
    {
        _guardiansService = guardiansService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<GuardianDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GuardianDto>>> GetAllAsync([FromQuery] SearchGuardians searchObject)
    {
        var guardians = await _guardiansService.SearchAsync(searchObject);
        return Ok(guardians);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuardianDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GuardianDto>> GetGuardianById(Guid id)
    {
        var guardian = await _guardiansService.GetByIdAsync(id);
        if (guardian == null)
            return NotFound("Guardian not found");
        var guardianDto = _mapper.Map<GuardianDto>(guardian);
        return Ok(guardianDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GuardianDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GuardianDto>> AddAsync(CreateGuardianDto requestObject)
    {
        var newGuardian = _mapper.Map<Guardian>(requestObject);
        var createdGuardian = await _guardiansService.InsertAsync(newGuardian);
        var createdGuardianDto = _mapper.Map<GuardianDto>(createdGuardian);
        return StatusCode(StatusCodes.Status201Created, createdGuardianDto);
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateGuardianDto updatedGuardian)
    {
        if (id != updatedGuardian.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingGuardian = await _guardiansService.GetByIdAsync(id);
            if (existingGuardian == null)
            {
                return NotFound("Guardian not found");
            }
            _mapper.Map(updatedGuardian, existingGuardian);
            await _guardiansService.UpdateAsync(existingGuardian);
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
    [HttpDelete()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync([FromQuery] List<Guid> ids)
    {
        try
        {
            await _guardiansService.DeleteRangeAsync(ids);
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

    [HttpGet("excel")]
    [Produces(typeof(FileContentResult))]
    public async Task<IActionResult> ExportToExcel([FromQuery] SearchGuardians search)
    {
        try
        {
            // Use filters to fetch data from your database
            var guardiansResult = await _guardiansService.SearchAsync(search);
            var data = guardiansResult.Entities.Select(g => new
            {
                g.FirstName,
                g.LastName,
                g.Gender,
                g.EmailAddress,
                g.PhoneNumber,
                g.Address,
                g.City,
                g.Country,
                g.Status
            });

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Guardians");
                worksheet.Cells["A1"].LoadFromCollection(data, true);

                // Adjust column headers
                worksheet.Cells["A1"].Value = "First Name";
                worksheet.Cells["B1"].Value = "Last Name";
                worksheet.Cells["C1"].Value = "Gender";
                worksheet.Cells["D1"].Value = "Email Address";
                worksheet.Cells["E1"].Value = "Phone Number";
                worksheet.Cells["F1"].Value = "Address";
                worksheet.Cells["G1"].Value = "City";
                worksheet.Cells["H1"].Value = "Country";
                worksheet.Cells["I1"].Value = "Status";

                // Bold the headers
                using (var range = worksheet.Cells["A1:I1"])
                {
                    range.Style.Font.Bold = true;
                }

                // Define a filter range for all columns from A to I
                worksheet.Cells["A1:I1"].AutoFilter = true;

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "guardians.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("pdf")]
    [Produces("application/pdf")]
    public async Task<IActionResult> ExportToPdf([FromQuery] SearchGuardians search)
    {
        try
        {
            // Use filters to fetch data from your database
            var guardiansResult = await _guardiansService.SearchAsync(search);
            var data = guardiansResult.Entities;

            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add a title
                var titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
                var titleParagraph = new Paragraph("Guardians Report", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(titleParagraph);

                // Add space between title and table
                document.Add(new Paragraph("\n"));

                // Add a table
                var table = new PdfPTable(9) { WidthPercentage = 100 };

                // Bold the headers
                var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                table.AddCell(new PdfPCell(new Phrase("First Name", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Last Name", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Gender", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Email Address", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Phone Number", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Address", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("City", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Country", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Status", headerFont)));

                foreach (var guardian in data)
                {
                    table.AddCell(guardian.FirstName);
                    table.AddCell(guardian.LastName);
                    table.AddCell(guardian.Gender);
                    table.AddCell(guardian.EmailAddress ?? string.Empty);
                    table.AddCell(guardian.PhoneNumber ?? string.Empty);
                    table.AddCell(guardian.Address ?? string.Empty);
                    table.AddCell(guardian.City);
                    table.AddCell(guardian.Country);
                    table.AddCell(guardian.Status);
                }

                document.Add(table);
                document.Close();

                var fileName = "guardians.pdf";
                var contentType = "application/pdf";

                return File(stream.ToArray(), contentType, fileName);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


}
