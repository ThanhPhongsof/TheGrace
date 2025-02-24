using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

public interface IProductCategoryBuilder
{
    IProductCategoryBuilder SetName(string name);

    IProductCategoryBuilder SetDescription(string description);

    IProductCategoryBuilder SetType(StatusEnum status);

    IProductCategoryBuilder SetSoftDelete(bool isInActive);

    IProductCategoryBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt);

    IProductCategoryBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt);

    ProductCategory Build();
}
