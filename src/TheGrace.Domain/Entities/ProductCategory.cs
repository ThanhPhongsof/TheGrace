using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Domain.Entities;

public class ProductCategory : EntityAudit<int>
{
    [Column(TypeName = "nvarchar(256)")]
    [MaxLength(256)]
    public string Name { get; private set; }

    [Column(TypeName = "nvarchar(4000)")]
    [MaxLength(4000)]
    public string Description { get; private set; }

    public int Type { get; private set; }

    public virtual ICollection<Product>? Products { get; private set; }

    public ProductCategory() { }

    public ProductCategory(int type, string name, string description, 
                           bool isInActive, string createdBy, DateTimeOffset createdAt, string updatedBy, DateTimeOffset updatedAt, int? id)
    {
        Id = id ?? 0;
        Type = type;
        Name = name;
        Description = description;
        IsInActive = isInActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
    }
}
