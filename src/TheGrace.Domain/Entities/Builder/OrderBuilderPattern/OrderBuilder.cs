using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.Builder.OrderLogBuilderPattern;
using TheGrace.Domain.Enumerations;
using TheGrace.Domain.Entities.Builder.CustomerBuilderPattern;

namespace TheGrace.Domain.Entities.Builder.OrderBuilderPattern;

public class OrderBuilder : IOrderBuilder
{
    public int Id { get; set; }

    public string Code { get; set; }

    public int Status { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly CreatedDate { get; set; }

    public int CustomerId { get; set; }

    public string StatusNote { get; set; }

    public int DeliveryPartnerId { get; set; }

    public bool IsPayment { get; set; }

    public bool ShipType { get; set; }

    public decimal ShipPrice { get; set; }

    public int DiscountType { get; set; }

    public decimal Discount { get; set; }

    public decimal DiscountPercent { get; set; }

    public decimal TotalDiscount { get; set; }

    public int TotalQuantity { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal TotalPayment { get; set; }

    public decimal TotalPoint { get; set; }

    public DateOnly? CompletedDate { get; set; }

    public string Note { get; set; }

    public string ItemProducts { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual DeliveryPartner DeliveryPartner { get; set; }

    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public IOrderBuilder SetCode(string code)
    {
        Code = code;
        return this;
    }

    public IOrderBuilder SetStatus(OrderStatusEnum orderStatus)
    {
        Status = orderStatus;
        return this;
    }

    public IOrderBuilder SetOrderDate(DateOnly orderDate)
    {
        OrderDate = orderDate;
        return this;
    }

    public IOrderBuilder SetCreatedDate(DateOnly createdDate)
    {
        CreatedDate = createdDate;
        return this;
    }

    public IOrderBuilder SetCompletedDate()
    {
        CompletedDate = null;
        return this;
    }

    public IOrderBuilder SetStatusNote(string statusNote)
    {
        StatusNote = statusNote;
        return this;
    }

    public IOrderBuilder SetIsPayment(bool isPayment)
    {
        IsPayment = isPayment;
        return this;
    }

    public IOrderBuilder SetShip(bool shipType, decimal shipPrice)
    {
        ShipType = shipType;
        ShipPrice = shipType ? 0 : shipPrice;
        return this;
    }

    public IOrderBuilder SetDiscount(int discountType, decimal discount, decimal discountPercent)
    {
        DiscountType = discountType;
        if (discountType == DiscountTypeEnum.Discount)
        {
            Discount = discount;
            DiscountPercent = 0;
        }
        else
        {
            Discount = 0;
            DiscountPercent = discountPercent;
        }

        return this;
    }

    public IOrderBuilder SetTotalQuantity(int totalQuantity)
    {
        TotalQuantity = totalQuantity;
        return this;
    }

    public IOrderBuilder SetTotalPrice(decimal totalPrice)
    {
        TotalPrice = totalPrice;
        return this;
    }

    public IOrderBuilder SetNote(string note)
    {
        Note = note;
        return this;
    }

    public IOrderBuilder SetItemProducts(string itemProducts)
    {
        ItemProducts = itemProducts;
        return this;
    }

    public IOrderBuilder SetCustomer(Customer customer)
    {
        Customer = customer;
        return this;
    }

    public IOrderBuilder SetDeliveryPartner(DeliveryPartner deliveryPartner)
    {
        DeliveryPartner = deliveryPartner;
        return this;
    }

    public IOrderBuilder SetSoftDelete(bool isInActive)
    {
        IsInActive = isInActive;
        return this;
    }

    public IOrderBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        return this;
    }

    public IOrderBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
        return this;
    }

    public OrderBuilder() { }

    public Order Build()
    {
        if (TotalQuantity == 0)
        {
            throw new InvalidOperationException("error quantity with 0");
        }

        TotalDiscount = ProcessTotalDiscount(DiscountType, Discount, DiscountPercent, TotalQuantity, TotalPrice);
        TotalPayment = ProcessTotalPaymentFinal(TotalQuantity, TotalPrice, ShipPrice, TotalDiscount);
        TotalPoint = ProcessTotalPointFinal(TotalPayment, Customer.Type);

        return new Order(
            Code, Status, OrderDate, CreatedDate,
            StatusNote, IsPayment, ShipType, ShipPrice,
            DiscountType, Discount, DiscountPercent, TotalDiscount,
            TotalQuantity, TotalPrice, TotalPayment, TotalPoint,
            Note, ItemProducts, CompletedDate,
            Customer, DeliveryPartner,
            IsInActive, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt,
            Id
            );
    }

    private decimal ProcessTotalPointFinal(decimal totalPayment, CustomerTypeEnum customerType)
    {
        decimal basePoint = totalPayment / 10000; // every 10.000 VND = 1 point
        return customerType == CustomerTypeEnum.VipCustomer ? basePoint * 1.5m : basePoint;
    }

    private decimal ProcessTotalPaymentFinal(int totalQuantity, decimal totalPrice, decimal shipPrice, decimal discountFinal)
    {
        return Math.Max((TotalQuantity * TotalPrice) - ShipPrice - discountFinal, 0);
    }

    private decimal ProcessTotalDiscount(DiscountTypeEnum discountType, decimal discount, decimal discountPercent, int totalQuantity, decimal totalPrice)
    {
        decimal discountFinal = discountType == DiscountTypeEnum.Discount
                ? discount
                : totalQuantity * totalPrice * discountPercent / 100;

        return discountFinal;
    }
}
