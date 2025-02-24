using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Domain.Entities;
public class OrderDetail : Entity<int>
{
    public int OrderId { get; private set; }

    public int ProductId { get; private set; }

    [Required]
    public int Quantity { get; private set; }

    public decimal Discount { get; private set; }

    public decimal Price { get; private set; }

    public decimal TotalPrice { get; private set; }

    public virtual Order Order { get; private set; }

    public virtual Product Product { get; private set; }

    public OrderDetail() { }

    public OrderDetail(
        int quantity, decimal discount, decimal price, decimal totalPrice,
        Order order, int productId, int? id)
    {
        Id = id ?? 0;
        Quantity = quantity;
        Discount = discount;
        Price = price;
        TotalPrice = totalPrice;
        OrderId = order.Id;
        ProductId = productId;
    }
}
