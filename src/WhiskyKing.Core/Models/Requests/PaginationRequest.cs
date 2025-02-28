namespace WhiskyKing.Core.Models.Requests;

public class PaginationRequest
{
    public string? Search { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public string OrderColumn { get; set; } = string.Empty;
    public bool OrderDescending { get; set; }
    public bool OnlyActive { get; set; }
}