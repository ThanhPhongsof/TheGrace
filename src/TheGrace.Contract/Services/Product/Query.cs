using TheGrace.Contract.Abstractions.Shared;

namespace TheGrace.Contract.Services.Product;

public static class Query
{
    public record GetProductsQuery(string? SearchTerm, int FilterType, bool FilterIsInDelete,
                                   string? SortColumn, SortOrder? SortOrder, IDictionary<string,
                                   SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ProductResponse>>;

    public record GetProductDetailQuery(int id) : IQuery<Response.ProductResponse>;
}
