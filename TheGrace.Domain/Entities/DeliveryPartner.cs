using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Domain.Entities;

public class DeliveryPartner : Entity<int>
{
    [Column(TypeName = "nvarchar(128)")]
    [MaxLength(128)]
    [Required]
    public string Name { get; private set; }

    [Required]
    public decimal Price { get; private set; }

    public virtual ICollection<Order> Orders { get; private set; }

    public DeliveryPartner() { }

    public DeliveryPartner(string name, decimal price, int? id)
    {
        Id = id ?? 0;
        Name = name;
        Price = price;
    }
}
