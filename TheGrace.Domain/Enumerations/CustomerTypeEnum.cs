using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace TheGrace.Domain.Enumerations;

public class CustomerTypeEnum : SmartEnum<CustomerTypeEnum>
{
    public string DisplayName { get; set; }

    public CustomerTypeEnum(string name, int value, string displayName) : base(name, value)
    {
        DisplayName = displayName;
    }

    #region ==========================  CustomerTypeEnum ==========================
    public static readonly CustomerTypeEnum NewCustomer = new(nameof(NewCustomer), 1, "New customer");
    public static readonly CustomerTypeEnum VipCustomer = new(nameof(VipCustomer), 2, "VIP customer");
    public static readonly CustomerTypeEnum BadCustomer = new(nameof(BadCustomer), 3, "Bad customer");
    public static readonly CustomerTypeEnum LoyalCustomer = new(nameof(LoyalCustomer), 4, "Loyal customer");
    public static readonly CustomerTypeEnum Blacklisted = new(nameof(Blacklisted), 0, "Black list");
    #endregion ==========================  CustomerTypeEnum ==========================

    public static implicit operator CustomerTypeEnum(string name) => FromName(name);

    public static implicit operator CustomerTypeEnum(int value) => FromValue(value);

    public static implicit operator string(CustomerTypeEnum type) => type.Name;

    public static implicit operator int(CustomerTypeEnum type) => type.Value;
}
