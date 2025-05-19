namespace SchoolManagementSystem.Api.Dtos.Dashboard;

public class DashboardDataDto
{
    public int TotalTeachers { get; set; }
    public int TotalStudents { get; set; }
    public int TotalGuardians { get; set; }
    public DashboardPieChartDto DashboardTeachersPieChartData { get; set; }
    public DashboardPieChartDto DashboardStudentsPieChartData { get; set; }
}   

