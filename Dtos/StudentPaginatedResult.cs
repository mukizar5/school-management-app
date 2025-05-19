namespace SchoolManagementSystem.Api.Dtos.Student
{
    public class StudentPaginatedResult : PaginatedResult<StudentDto>
    {
        public int MaleStudentsCount { get; set; }
        public int FemaleStudentsCount { get; set; }
    }
}