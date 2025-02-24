using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace TheGrace.Domain.Enumerations;

public class OrderStatusEnum : SmartEnum<OrderStatusEnum>
{
    public OrderStatusEnum(string name, int value) : base(name, value) { }

    #region ==========================  OrderStatusEnum ==========================
    public static readonly OrderStatusEnum New = new(nameof(OrderStatusEnum), 1);
    public static readonly OrderStatusEnum Pending = new(nameof(OrderStatusEnum), 2);
    public static readonly OrderStatusEnum Packaged = new(nameof(OrderStatusEnum), 3);
    public static readonly OrderStatusEnum Delivery = new(nameof(OrderStatusEnum), 4);
    public static readonly OrderStatusEnum Complete = new(nameof(OrderStatusEnum), 5);
    public static readonly OrderStatusEnum CancelDelivery = new (nameof(OrderStatusEnum), 6);
    public static readonly OrderStatusEnum Cancel = new (nameof(OrderStatusEnum), 7);
    #endregion ==========================  OrderStatusEnum ==========================

    public static implicit operator OrderStatusEnum(string name) => FromName(name);

    public static implicit operator OrderStatusEnum(int value) => FromValue(value);

    public static implicit operator string(OrderStatusEnum status) => status.Name;

    public static implicit operator int(OrderStatusEnum status) => status.Value;
}
