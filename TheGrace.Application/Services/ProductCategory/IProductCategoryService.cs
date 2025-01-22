using TheGrace.Application.Abstractions.Shared;
using TheGrace.Domain.Contract.ProductCategory;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

namespace TheGrace.Application.Services.ProductCategory;

public interface IProductCategoryService
{
    Task<Result<IEnumerable<ProductCategoryResponse>>> GetProductCategories();

    Task<Result> CreateProductCategories();
}
