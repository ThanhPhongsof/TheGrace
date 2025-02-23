using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities.Builder.OrderBuilderPattern;

public interface IOrderBuilder
{
    IOrderBuilder SetCode(string code);

    IOrderBuilder SetStatus(OrderStatusEnum orderStatus);

    IOrderBuilder SetOrderDate(DateOnly orderDate);

    IOrderBuilder SetCreatedDate(DateOnly createdDate);

    IOrderBuilder SetCompletedDate();

    IOrderBuilder SetStatusNote(string statusNote);

    IOrderBuilder SetIsPayment(bool isPayment);

    IOrderBuilder SetShip(bool shipType, decimal shipPrice);

    IOrderBuilder SetDiscount(int discountType, decimal discount, decimal discountPercent);

    IOrderBuilder SetTotalQuantity(int totalQuantity);

    IOrderBuilder SetTotalPrice(decimal totalPrice);

    IOrderBuilder SetNote(string note);

    IOrderBuilder SetItemProducts(string itemProducts);

    IOrderBuilder SetCustomer(Customer customer);

    IOrderBuilder SetDeliveryPartner(DeliveryPartner deliveryPartner);

    IOrderBuilder SetSoftDelete(bool isInActive);

    IOrderBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt);

    IOrderBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt);

    Order Build();
}
