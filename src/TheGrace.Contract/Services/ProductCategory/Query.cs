using TheGrace.Contract.Abstractions.Shared;
using static TheGrace.Contract.Services.ProductCategory.Response;

namespace TheGrace.Contract.Services.ProductCategory;
public class Query
{
    public record GetProductCategoriesQuery() : IQuery<IEnumerable<ProductCategoryResponse>>;
}
