namespace WhiskyKing.Core.Models.Responses;

public class PaginationRepositoryResponse<T> where T : class
{
    public IEnumerable<T> Data { get; set; } = [];
    public int TotalCount { get; set; }
}