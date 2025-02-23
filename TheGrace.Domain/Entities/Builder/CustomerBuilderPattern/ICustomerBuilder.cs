using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.CustomerBuilderPattern;

public interface ICustomerBuilder
{
    ICustomerBuilder SetType(CustomerTypeEnum type);

    ICustomerBuilder SetName(string name);

    ICustomerBuilder SetPhonePrimary(string phonePrimary);

    ICustomerBuilder SetPhoneSecond(string phoneSecond);

    ICustomerBuilder SetAddress(string address);

    ICustomerBuilder SetSoftDelete(bool isInActive);

    ICustomerBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt);

    ICustomerBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt);

    Customer Build();
}
