using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Linq;
using TheGrace.Domain.Entities.EntityBase;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities;

public class Product : EntityAudit<int>
{
    public int ProductCategoryId { get; private set; }

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

    public virtual ICollection<OrderDetail> OrderDetails { get; private set; }

    public Product() { }

    public Product(int type, string code, string name, string image, string description, decimal price, int quantity,
                   bool isInActive, string createdBy, DateTimeOffset createdAt, string updatedBy, DateTimeOffset updatedAt,
                   ProductCategory productCategory, int? id)
    {
        Id = id ?? 0;
        Type = type;
        Code = code;
        Name = name;
        Image = image;
        Description = description;
        Price = price;
        Quantity = quantity;
        IsInActive = isInActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        //ProductCategory = productCategory;
        ProductCategoryId = productCategory.Id;
    }

    public void Update(StatusEnum type, string name, string description, decimal price, int quantity, string updatedBy, DateTimeOffset updatedAt)
    {
        Type = type;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
    }

    public void UpdateSoftDelete(bool isInActive, string updatedBy, DateTimeOffset updatedAt)
    {
        IsInActive = isInActive;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
    }
}
