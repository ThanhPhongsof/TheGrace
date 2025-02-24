using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Entities.Builder.DeliveryPartnerBuilderPattern;

public interface IDeliveryPartnerBuilder
{
    IDeliveryPartnerBuilder SetName(string name);

    IDeliveryPartnerBuilder SetPrice(decimal price);

    DeliveryPartner Build();
}
