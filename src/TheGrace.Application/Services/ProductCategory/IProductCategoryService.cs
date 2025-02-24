using TheGrace.Contract.Services.ProductCategory;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

namespace TheGrace.Application.Services.ProductCategory;

public interface IProductCategoryService
{
    Task<IEnumerable<Response.ProductCategoryResponse>> GetProductCategories();

    //Task<Result> CreateProductCategories();
}
