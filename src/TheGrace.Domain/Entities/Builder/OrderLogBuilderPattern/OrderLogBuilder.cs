using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.OrderLogBuilderPattern;
public class OrderLogBuilder : IOrderLogBuilder
{

    public int Id { get; set; }

    public int Status { get; set; }

    public string StatusNote { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Order Order { get; set; }

    public OrderLogBuilder() { }

    public IOrderLogBuilder SetCreatedAt(string createdBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        return this;
    }

    public IOrderLogBuilder SetStatus(OrderStatusEnum status, string statusNote)
    {
        Status = status;
        StatusNote = statusNote;
        return this;
    }

    public IOrderLogBuilder SetOrder(Order order)
    {
        Order = order;
        return this;
    }

    public OrderLog Build()
    {
        return new OrderLog(Status, StatusNote, CreatedAt, CreatedBy, Order, Id);
    }
}
