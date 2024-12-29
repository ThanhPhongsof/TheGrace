using TheGrace.Domain.Abstractions;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.ProductBuilderPattern;

public class ProductBuilder : IProductBuilder
{
    public Guid Id { get; set; }

    public int Type { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ProductCategory ProductCategory { get; set; }

    public ProductBuilder()
    {
        Id = SequentialGuid.NewGuid();
    }

    public IProductBuilder SetProductCategory(ProductCategory productCategory)
    {
        ProductCategory = productCategory;
        return this;
    }

    public IProductBuilder SetType(StatusEnum status)
    {
        Type = status.Value;
        return this;
    }

    public IProductBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public IProductBuilder SetCode(string code)
    {
        Code = code;
        return this;
    }

    public IProductBuilder SetDescription(string description)
    {
        Description = description;
        return this;
    }

    public IProductBuilder SetImage(string image)
    {
        Image = image;
        return this;
    }

    public IProductBuilder SetPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public IProductBuilder SetQuantity(int quantity)
    {
        Quantity = quantity;
        return this;
    }

    public IProductBuilder SetSoftDelete(bool isInActive)
    {
        IsInActive = isInActive;
        return this;
    }

    public IProductBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        return this;
    }

    public IProductBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
        return this;
    }

    public Product Build()
    {
        return new Product(Type, Code, Name, Image, Description, Price, Quantity, 
                           IsInActive, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, 
                           ProductCategory, 0);
    }
}
