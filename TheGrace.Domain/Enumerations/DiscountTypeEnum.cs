using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace TheGrace.Domain.Enumerations;

public class DiscountTypeEnum : SmartEnum<DiscountTypeEnum>
{
    public DiscountTypeEnum(string name, int value): base(name, value) { }

    #region ==========================  DiscountTypeEnum ==========================
    public static readonly DiscountTypeEnum Discount = new(nameof(Discount), 0);
    public static readonly DiscountTypeEnum DiscountPercent = new(nameof(DiscountPercent), 1);
    #endregion ==========================  DiscountTypeEnum ==========================

    public static implicit operator DiscountTypeEnum(string name) => FromName(name);

    public static implicit operator DiscountTypeEnum(int value) => FromValue(value);

    public static implicit operator string(DiscountTypeEnum type) => FromName(type.Name);

    public static implicit operator int(DiscountTypeEnum type) => FromValue(type.Value);

}
