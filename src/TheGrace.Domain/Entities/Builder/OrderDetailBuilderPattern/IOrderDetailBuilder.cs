using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Entities.Builder.OrderDetailBuilderPattern;

public interface IOrderDetailBuilder
{
    IOrderDetailBuilder SetQuantity(int quantity);

    IOrderDetailBuilder SetDiscount(decimal discount);

    IOrderDetailBuilder SetPrice(decimal price);

    IOrderDetailBuilder SetProductId(int productId);

    IOrderDetailBuilder SetOrder(Order order);

    OrderDetail Build();
}
