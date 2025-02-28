namespace WhiskyKing.Core.Models.Responses;

public class PaginationResponse<T> where T : class
{
    public IEnumerable<T> Data { get; set; } = [];
    public int RecordsTotal { get; set; }
}