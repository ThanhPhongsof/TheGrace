using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

public class ProductCategoryBuilder : IProductCategoryBuilder
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Type { get; set; }

    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public ProductCategoryBuilder()
    {
        Id = Guid.NewGuid();
        CreatedBy = "system";
        UpdatedBy = CreatedBy;
    }

    public IProductCategoryBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public IProductCategoryBuilder SetDescription(string description)
    {
        Description = description ?? "";
        return this;
    }

    public IProductCategoryBuilder SetType(StatusEnum status)
    {
        Type = status.Value;
        return this;
    }

    public IProductCategoryBuilder SetSoftDelete(bool isInActive)
    {
        IsInActive = isInActive;
        return this;
    }

    public IProductCategoryBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        return this;
    }

    public IProductCategoryBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
        return this;
    }

    public ProductCategory Build()
    {
        return new ProductCategory(Id, Type, Name, Description, IsInActive, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt);
    }
}
