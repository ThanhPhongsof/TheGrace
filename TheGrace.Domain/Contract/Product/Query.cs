using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Contract.Product;

public static class Query
{
    public record GetProductsQuery(string? SearchTerm, int FilterType, bool FilterIsInDelete, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize);

    public record GetProductQuery(int id);
}
