namespace SchoolManagementSystem.Api.Dtos.Dashboard;

public class DashboardPieChartDto
{
   public IEnumerable<int> Series { get; set; }
   public IEnumerable<string> Labels { get; set; } 
}