
namespace SchoolManagementSystem.Api.Dtos;

public abstract class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}


