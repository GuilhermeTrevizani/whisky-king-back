using System.Linq.Expressions;

namespace WhiskyKing.Core.Models.Requests;

public class PaginationRepositoryRequest<T> where T : class
{
    public PaginationRepositoryRequest(
        PaginationRequest paginationRequest,
        Dictionary<string, Expression<Func<T, object>>> dictionaryFromTo,
        Expression<Func<T, bool>>? whereExpression,
        IEnumerable<string>? includes = null
        )
    {
        dictionaryFromTo.TryGetValue(paginationRequest.OrderColumn.ToLower(), out Expression<Func<T, object>>? orderExpression);
        OrderExpression = orderExpression ?? dictionaryFromTo.FirstOrDefault().Value;

        WhereExpression = whereExpression;
        Skip = paginationRequest.Skip;
        Take = paginationRequest.Take;
        OrderDescending = paginationRequest.OrderDescending;
        OnlyActive = paginationRequest.OnlyActive;

        Includes = includes ?? [];
    }

    public Expression<Func<T, bool>>? WhereExpression { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public Expression<Func<T, object>> OrderExpression { get; set; }
    public bool OrderDescending { get; set; }
    public bool OnlyActive { get; set; }
    public IEnumerable<string> Includes { get; set; }
}