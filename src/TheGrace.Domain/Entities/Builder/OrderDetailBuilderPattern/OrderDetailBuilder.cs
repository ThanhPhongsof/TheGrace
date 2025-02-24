using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Entities.Builder.OrderDetailBuilderPattern;

public class OrderDetailBuilder : IOrderDetailBuilder
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public int ProductId { get; set; }

    public virtual Order Order { get; set; }

    public IOrderDetailBuilder SetQuantity(int quantity)
    {
        Quantity = quantity;
        return this;
    }

    public IOrderDetailBuilder SetDiscount(decimal discount)
    {
        Discount = discount;
        return this;
    }

    public IOrderDetailBuilder SetPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public IOrderDetailBuilder SetProductId(int productId)
    {
        ProductId = productId;
        return this;
    }

    public IOrderDetailBuilder SetOrder(Order order)
    {
        Order = order;
        return this;
    }

    public OrderDetailBuilder() { }

    public OrderDetail Build()
    {
        if (Quantity <= 0)
        {
            throw new InvalidOperationException("error quantity with 0");
        }

        decimal total = Quantity * Price;

        TotalPrice = Discount > total ? 0 : total - Discount;

        return new OrderDetail(Quantity, Discount, Price, TotalPrice, Order, ProductId, Id);
    }
}
