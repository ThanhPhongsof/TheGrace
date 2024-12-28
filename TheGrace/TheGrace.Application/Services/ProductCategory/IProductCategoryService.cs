using TheGrace.Application.Abstractions.Shared;
using TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

namespace TheGrace.Application.Services.ProductCategory;

public interface IProductCategoryService
{
    Task GetAll();

    Task<Result> CreateProductCategories();

    Task CreateOrUpdate(ProductCategoryBuilder productCategory);
}
