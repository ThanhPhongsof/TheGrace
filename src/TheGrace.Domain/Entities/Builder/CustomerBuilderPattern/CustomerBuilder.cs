using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.CustomerBuilderPattern;

public class CustomerBuilder : ICustomerBuilder
{
    public int Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; }

    public string PhonePrimary { get; set; }

    public string PhoneSecond { get; set; }

    public string Address { get; set; }

    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public ICustomerBuilder SetType(CustomerTypeEnum customerType)
    {
        Type = customerType;
        return this;
    }

    public ICustomerBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public ICustomerBuilder SetPhonePrimary(string phonePrimary)
    {
        PhonePrimary = phonePrimary;
        return this;
    }

    public ICustomerBuilder SetPhoneSecond(string phoneSecond)
    {
        PhoneSecond = phoneSecond;
        return this;
    }

    public ICustomerBuilder SetAddress(string address)
    {
        Address = address;
        return this;
    }

    public ICustomerBuilder SetSoftDelete(bool isInActive)
    {
        IsInActive = isInActive;
        return this;
    }

    public ICustomerBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        return this;
    }

    public ICustomerBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
        return this;
    }

    public CustomerBuilder() { }

    public Customer Build()
    {
        return new Customer(Type, Name, PhonePrimary, PhoneSecond, Address, 
                            IsInActive, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, Id);
    }
}
