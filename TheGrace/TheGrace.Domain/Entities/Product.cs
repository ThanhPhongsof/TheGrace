using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Domain.Entities;

public class Product : EntityAudit<Guid>
{
    public Guid ProductCategoryId { get; private set; }

    public int Type { get; private set; }

    [Column(TypeName = "varchar(20)")]
    [MaxLength(20)]
    public string Code { get; private set; }

    [Column(TypeName = "nvarchar(256)")]
    [MaxLength(256)]
    public string Name { get; private set; }

    [Column(TypeName = "varchar(MAX)")]
    public string Image { get; private set; }

    [Column(TypeName = "nvarchar(256)")]
    [MaxLength(256)]
    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public virtual ProductCategory ProductCategory { get; private set; }

    public Product() { }

    public Product(Guid id, int type, string code, string name, string image, string description, decimal price, int quantity, ProductCategory productCategory)
    {
        Id = Id;
        ProductCategoryId = productCategory.Id;
        Type = type;
        Code = code;
        Name = name;
        Image = image;
        Description = description;
        Price = price;
        Quantity = quantity;
        ProductCategory = productCategory;
    }
}
