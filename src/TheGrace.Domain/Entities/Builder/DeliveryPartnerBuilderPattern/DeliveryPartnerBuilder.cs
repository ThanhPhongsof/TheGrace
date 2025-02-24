using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Entities.Builder.DeliveryPartnerBuilderPattern;

public class DeliveryPartnerBuilder : IDeliveryPartnerBuilder
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public DeliveryPartnerBuilder() { }

    public IDeliveryPartnerBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public IDeliveryPartnerBuilder SetPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public DeliveryPartner Build()
    {
        return new DeliveryPartner(Name, Price, Id);
    }
}
