namespace SchoolManagementSystem.Api.Dtos.Teacher
{
    public class TeacherPaginatedResult : PaginatedResult<TeacherDto>
    {
        public int MaleTeachersCount { get; set; }
        public int FemaleTeachersCount { get; set; }
    }
}