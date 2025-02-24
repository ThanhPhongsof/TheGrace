using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.EntityBase;
using System.ComponentModel;

namespace TheGrace.Domain.Entities;

public class Order : EntityAudit<int>
{
    public string Code { get; private set; }

    [DefaultValue(1)]
    public int Status { get; private set; }

    [Required]
    public DateOnly OrderDate { get; private set; }

    [Required]
    public DateOnly CreatedDate { get; private set; }

    public int CustomerId { get; private set; }

    [Required]
    public string StatusNote { get; private set; }

    public int DeliveryPartnerId { get; private set; }

    public bool IsPayment { get; set; }

    public bool ShipType { get; private set; }

    public decimal ShipPrice { get; private set; }

    public int DiscountType { get; private set; }

    public decimal Discount { get; private set; }

    public decimal DiscountPercent { get; private set; }

    public decimal TotalDiscount { get; private set; }

    [Required]
    public int TotalQuantity { get; private set; }

    public decimal TotalPrice { get; private set; }

    public decimal TotalPayment { get; private set; }

    public decimal TotalPoint { get; private set; }

    public DateOnly? CompletedDate { get; private set; }

    [Column(TypeName = "nvarchar(4000)")]
    [MaxLength(4000)]
    public string Note { get; private set; }

    [Column(TypeName = "nvarchar(max)")]
    [Required]
    public string ItemProducts { get; private set; }

    public virtual Customer Customer { get; private set; }

    public virtual DeliveryPartner DeliveryPartner { get; private set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; private set; }

    public virtual ICollection<OrderLog> OrderLogs { get; private set; }

    public Order() { }

    public Order(
        string code, int status, DateOnly orderDate, DateOnly createdDate,
        string statusNote, bool isPayment, bool shipType, decimal shipPrice,
        int discountType, decimal discount, decimal discountPercent, decimal totalDiscount,
        int totalQuantity, decimal totalPrice, decimal totalPayment, decimal totalPoint,
        string note, string itemProducts, DateOnly? completedDate,
        Customer customer, DeliveryPartner deliveryPartner,
        bool isInActive, string createdBy, DateTimeOffset createdAt, string updatedBy, DateTimeOffset updatedAt,
        int? id
        )
    {
        Id = id ?? 0;
        Code = code;
        Status = status;
        OrderDate = orderDate;
        CreatedDate = createdDate;
        StatusNote = statusNote;
        IsPayment = isPayment;
        ShipType = shipType;
        ShipPrice = shipPrice;
        DiscountType = discountType;
        Discount = discount;
        DiscountPercent = discountPercent;
        TotalDiscount = totalDiscount;
        TotalQuantity = totalQuantity;
        TotalPrice = totalPrice;
        TotalPayment = totalPayment;
        CompletedDate = completedDate;
        Note = note;
        ItemProducts = itemProducts;
        IsInActive = isInActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        TotalPoint = totalPoint;
        CustomerId = customer.Id;
        DeliveryPartnerId = deliveryPartner.Id;
    }

    public void Update(
        DateOnly orderDate,
        int customerId, bool shipType, decimal shipPrice,
        int discountType, decimal discount, decimal discountPercent, decimal totalDiscount,
        int totalQuantity, decimal totalPrice, decimal totalPayment, decimal totalPoint,
        string note, string itemProducts, DeliveryPartner deliveryPartner,
        string updatedBy, DateTimeOffset updatedAt
        )
    {
        OrderDate = orderDate;
        ShipType = shipType;
        ShipPrice = shipPrice;
        DiscountType = discountType;
        Discount = discount;
        DiscountPercent = discountPercent;
        TotalDiscount = totalDiscount;
        TotalQuantity = totalQuantity;
        TotalPrice = totalPrice;
        TotalPayment = totalPayment;
        TotalPoint = totalPoint;
        Note = note;
        ItemProducts = itemProducts;
        DeliveryPartnerId = deliveryPartner.Id;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}
