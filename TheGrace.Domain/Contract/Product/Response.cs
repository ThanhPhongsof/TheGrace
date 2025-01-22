using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities;
using TheGrace.Domain.Entities.EntityBase;
using TheGrace.Domain.Contract.ProductCategory;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Contract.Product;
public class ProductResponse : EntityAudit<int>
{
    public int ProductCategoryId { get; set; }

    public StatusEnum Type { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public ProductCategoryResponse? Category { get; set; }
}
