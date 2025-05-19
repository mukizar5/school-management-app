using System.Linq.Expressions;
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SchoolManagementSystem.Api.Dtos;
using SchoolManagementSystem.Api.Dtos.Teacher;
using SchoolManagementSystem.Api.Enums;
using SchoolManagementSystem.Api.Models;
using SchoolManagementSystem.Api.SearchObjects;
using SchoolManagementSystem.Api.Services.Teachers;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersService _teachersService;
        private readonly IMapper _mapper;

        public TeachersController(ITeachersService teachersService, IMapper mapper)
        {
            _teachersService = teachersService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherPaginatedResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherPaginatedResult>> GetAllAsync([FromQuery] SearchTeachers searchObject)
        {
            try
            {
                var teachers = await _teachersService.SearchAsync(searchObject);
                var maleTeachersCount = teachers.Count(t => t.Gender == Gender.Male);
                var femaleTeachersCount = teachers.Count(t => t.Gender == Gender.Female);
                var paginatedTeacherDtoList = _mapper.Map<List<TeacherDto>>(teachers);
                var paginatedResult = new TeacherPaginatedResult
                {
                    Entities = paginatedTeacherDtoList,
                    PaginationMetadata = new PaginationMetadata
                    {
                        Page = teachers.Page,
                        PageSize = teachers.PageSize,
                        TotalPages = teachers.TotalPages,
                        TotalCount = teachers.TotalCount
                    },
                    MaleTeachersCount = maleTeachersCount,
                    FemaleTeachersCount = femaleTeachersCount
                };
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(Guid id)
        {
            var teacher = await _teachersService.GetByIdAsync(id);
            if (teacher == null)
                return NotFound("Teacher not found");
            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return Ok(teacherDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherDto>> AddAsync(CreateTeacherDto requestObject)
        {
            try
            {
                var newTeacherId = Guid.NewGuid();
                var newTeacher = _mapper.Map<Teacher>(requestObject);
                newTeacher.Id = newTeacherId;
                if (requestObject.SubjectIds != null)
                {
                    newTeacher.Subjects = requestObject.SubjectIds.Select(id => new TeacherSubject { TeacherId = newTeacherId, SubjectId = id }).ToList();
                }
                if (requestObject.ClassIds != null)
                {
                    newTeacher.Classes = requestObject.ClassIds.Select(id => new ClassTeacher { TeacherId = newTeacherId, ClassId = id }).ToList();
                }
                var createdTeacher = await _teachersService.InsertAsync(newTeacher);
                var createdTeacherDto = _mapper.Map<TeacherDto>(createdTeacher);
                return StatusCode(StatusCodes.Status201Created, createdTeacherDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateTeacherDto updatedTeacher)
        {
            if (id != updatedTeacher.Id)
            {
                return BadRequest("ID in the route does not match the ID in the body.");
            }
            try
            {
                var existingTeacherResult = await _teachersService.SearchAsync(new SearchTeachers { Id = id });
                var existingTeacher = existingTeacherResult.FirstOrDefault();
                if (existingTeacher == null)
                {
                    return NotFound("Teacher not found");
                }
                var existingSubjects = existingTeacher.Subjects.ToList() ?? new List<TeacherSubject>();
                var existingClasses = existingTeacher.Classes.ToList() ?? new List<ClassTeacher>();
                _mapper.Map(updatedTeacher, existingTeacher);
                foreach (var existingSubject in existingSubjects)
                {
                    if (!updatedTeacher.SubjectIds.Any(id => id == existingSubject.SubjectId))
                    {
                        existingTeacher.Subjects.Remove(existingSubject);
                    }
                }

                foreach (var subjectId in updatedTeacher.SubjectIds)
                {
                    if (!existingTeacher.Subjects.Any(s => s.SubjectId == subjectId))
                    {
                        existingTeacher.Subjects.Add(new TeacherSubject { TeacherId = existingTeacher.Id, SubjectId = subjectId });
                    }
                }

                foreach (var existingClass in existingClasses)
                {
                    if (!updatedTeacher.ClassIds.Any(id => id == existingClass.ClassId))
                    {
                        existingTeacher.Classes.Remove(existingClass);
                    }
                }

                foreach (var classId in updatedTeacher.ClassIds)
                {
                    if (!existingTeacher.Classes.Any(c => c.ClassId == classId))
                    {
                        existingTeacher.Classes.Add(new ClassTeacher { TeacherId = existingTeacher.Id, ClassId = classId });
                    }
                }
                await _teachersService.UpdateAsync(existingTeacher);
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync([FromQuery] List<Guid> ids)
        {
            try
            {
                await _teachersService.DeleteRangeAsync(ids);
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
        public async Task<IActionResult> ExportToExcel([FromQuery] SearchTeachers search)
        {
            try
            {
                // Use filters to fetch data from your database
                var teachers = await _teachersService.SearchAsync(search);
                var data = teachers.Select(t => new
                {
                    t.FirstName,
                    t.LastName,
                    t.Gender,
                    t.DateOfBirth,
                    t.EmploymentDate,
                    t.ExitDate,
                    t.Status,
                    t.MarriageStatus,
                    t.Salary,
                    t.EmailAddress,
                    t.PhoneNumber,
                    t.Address,
                    t.City,
                    t.Country
                });

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Teachers");
                    worksheet.Cells["A1"].LoadFromCollection(data, true);

                    // Adjust column headers
                    worksheet.Cells["A1"].Value = "First Name";
                    worksheet.Cells["B1"].Value = "Last Name";
                    worksheet.Cells["C1"].Value = "Gender";
                    worksheet.Cells["D1"].Value = "Date of Birth";
                    worksheet.Cells["E1"].Value = "Employment Date";
                    worksheet.Cells["F1"].Value = "Exit Date";
                    worksheet.Cells["G1"].Value = "Status";
                    worksheet.Cells["H1"].Value = "Marriage Status";
                    worksheet.Cells["I1"].Value = "Salary";
                    worksheet.Cells["J1"].Value = "Email Address";
                    worksheet.Cells["K1"].Value = "Phone Number";
                    worksheet.Cells["L1"].Value = "Address";
                    worksheet.Cells["M1"].Value = "City";
                    worksheet.Cells["N1"].Value = "Country";

                    // Bold the headers
                    using (var range = worksheet.Cells["A1:N1"])
                    {
                        range.Style.Font.Bold = true;
                    }

                    // Format the Donation Date column as Date
                    worksheet.Column(4).Style.Numberformat.Format = "MM/dd/yyyy";
                    worksheet.Column(5).Style.Numberformat.Format = "MM/dd/yyyy";
                    worksheet.Column(6).Style.Numberformat.Format = "MM/dd/yyyy";

                    // Format the Amount column with comma separators
                    worksheet.Column(9).Style.Numberformat.Format = "#,##0";

                    // Define a filter range for all columns from C to N
                    worksheet.Cells["C1:N1"].AutoFilter = true;

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    var fileName = "teachers.xlsx";
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
        public async Task<IActionResult> ExportToPdf([FromQuery] SearchTeachers search)
        {
            try
            {
                // Use filters to fetch data from your database
                var teachers = await _teachersService.SearchAsync(search);
                var data = teachers;

                using (var stream = new MemoryStream())
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Add a title
                    var titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
                    var titleParagraph = new Paragraph("Teachers Report", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    document.Add(titleParagraph);

                    // Add space between title and table
                    document.Add(new Paragraph("\n"));

                    // Add a table
                    var table = new PdfPTable(13) { WidthPercentage = 100 };

                    // Bold the headers
                    var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                    table.AddCell(new PdfPCell(new Phrase("First Name", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Last Name", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Gender", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Date of Birth", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Employment Date", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Exit Date", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Marriage Status", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Salary", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Email Address", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Phone Number", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Address", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("City", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Country", headerFont)));

                    foreach (var teacher in data)
                    {
                        table.AddCell(teacher.FirstName);
                        table.AddCell(teacher.LastName);
                        table.AddCell(teacher.Gender.ToString());
                        table.AddCell(teacher.DateOfBirth?.ToString("MM/dd/yyyy") ?? string.Empty);
                        table.AddCell(teacher.EmploymentDate.ToString("MM/dd/yyyy"));
                        table.AddCell(teacher.ExitDate?.ToString("MM/dd/yyyy") ?? string.Empty);
                        table.AddCell(teacher.MarriageStatus.ToString());
                        table.AddCell(teacher.Salary.ToString("#,##0"));
                        table.AddCell(teacher.EmailAddress ?? string.Empty);
                        table.AddCell(teacher.PhoneNumber ?? string.Empty);
                        table.AddCell(teacher.Address ?? string.Empty);
                        table.AddCell(teacher.City);
                        table.AddCell(teacher.Country);
                    }

                    document.Add(table);
                    document.Close();

                    var fileName = "teachers.pdf";
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


}
