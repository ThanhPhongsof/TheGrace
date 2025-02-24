using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.OrderLogBuilderPattern;

public interface IOrderLogBuilder
{
    IOrderLogBuilder SetStatus(OrderStatusEnum status, string statusNote);

    IOrderLogBuilder SetOrder(Order order);

    IOrderLogBuilder SetCreatedAt(string createdBy, DateTimeOffset createdAt);

    OrderLog Build();
}
