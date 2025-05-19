namespace SchoolManagementSystem.Api.Dtos
{
    public class PaginatedResult<T>
    {
        public List<T> Entities { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }
    }
    public class PaginationMetadata
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

}