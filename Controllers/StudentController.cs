using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SchoolManagementSystem.Api.Dtos;
using SchoolManagementSystem.Api.Dtos.Student;
using SchoolManagementSystem.Api.Enums;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Services.Students;

namespace SchoolManagementSystem.Api.Controllers;

[ApiController]
[Route("students")]  // Updated the route to plural to match conventions
public class StudentController : ControllerBase
{
    private readonly IStudentsService _studentsService;
    private readonly IMapper _mapper;

    public StudentController(IStudentsService studentsService, IMapper mapper)
    {
        _studentsService = studentsService;
        _mapper = mapper;
    }

    // GET: students
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentPaginatedResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentPaginatedResult>> GetAllAsync([FromQuery] SearchStudents searchObject)
    {
        var students = await _studentsService.SearchAsync(searchObject);
        var maleStudentsCount = students.Count(t => t.Gender == Gender.Male);
        var femaleStudentsCount = students.Count(t => t.Gender == Gender.Female);
        var paginatedStudentDtoList = _mapper.Map<List<StudentDto>>(students);
        var paginatedResult = new StudentPaginatedResult
        {
            Entities = paginatedStudentDtoList,
            PaginationMetadata = new PaginationMetadata
            {
                Page = students.Page,
                PageSize = students.PageSize,
                TotalPages = students.TotalPages,
                TotalCount = students.TotalCount
            },
            MaleStudentsCount = maleStudentsCount,
            FemaleStudentsCount = femaleStudentsCount
        };
        return Ok(paginatedResult);
    }

    // GET: students/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentDto>> GetStudentById(Guid id)
    {
        var student = await _studentsService.GetByIdAsync(id);
        if (student == null)
            return NotFound("Student not found");
        var studentDto = _mapper.Map<StudentDto>(student);
        return Ok(studentDto);
    }

    // POST: students
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StudentDto>> AddAsync(CreateStudentDto requestObject)
    {
        try{
        var newStudent = _mapper.Map<Student>(requestObject);
        var createdStudent = await _studentsService.InsertAsync(newStudent);
        var createdStudentDto = _mapper.Map<StudentDto>(createdStudent);
        return StatusCode(StatusCodes.Status201Created, createdStudentDto);

        }catch(Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // PUT: students/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateStudentDto updatedStudent)
    {
        if (id != updatedStudent.Id)
        {
            return BadRequest("ID in the route does not match the ID in the body.");
        }
        try
        {
            var existingStudent = await _studentsService.GetByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound("Student not found");
            }
            _mapper.Map(updatedStudent, existingStudent);
            await _studentsService.UpdateAsync(existingStudent);
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
            await _studentsService.DeleteRangeAsync(ids);
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
    public async Task<IActionResult> ExportToExcel([FromQuery] SearchStudents search)
    {
        try
        {
            // Use filters to fetch data from your database
            var studentsResult = await _studentsService.SearchAsync(search);
            var data = studentsResult.Select(s => new
            {
                s.FirstName,
                s.LastName,
                s.Gender,
                s.Status
            });

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Students");
                worksheet.Cells["A1"].LoadFromCollection(data, true);

                // Adjust column headers
                worksheet.Cells["A1"].Value = "First Name";
                worksheet.Cells["B1"].Value = "Last Name";
                worksheet.Cells["C1"].Value = "Gender";
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

                var fileName = "students.xlsx";
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
    public async Task<IActionResult> ExportToPdf([FromQuery] SearchStudents search)
    {
        try
        {
            // Use filters to fetch data from your database
            var studentsResult = await _studentsService.SearchAsync(search);
            var data = studentsResult;

            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add a title
                var titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
                var titleParagraph = new Paragraph("Students Report", titleFont)
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
                table.AddCell(new PdfPCell(new Phrase("Status", headerFont)));

                foreach (var student in data)
                {
                    table.AddCell(student.FirstName);
                    table.AddCell(student.LastName);
                    table.AddCell(student.Gender.ToString());
                    table.AddCell(student.Status.ToString());
                }

                document.Add(table);
                document.Close();

                var fileName = "students.pdf";
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
