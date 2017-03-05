using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AnythingQA.Model
{
    public class Order
    {
        string id;
        string cart;
        string deliveryAddress;
        double amount;
        OrderStatus orderStatus;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "cartId")]
        public string Cart
        {
            get { return cart; }
            set { cart = value; }
        }

        [JsonProperty(PropertyName = "deliveryAddress")]
        public string DeliveryAddress
        {
            get { return deliveryAddress; }
            set { deliveryAddress = value; }
        }

        [JsonProperty(PropertyName = "amountToBePaid")]
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [JsonProperty(PropertyName = "orderStatus")]
        public OrderStatus OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

        [Version]
        public string Version { get; set; }
    }

    public enum OrderStatus
    {
        CustomerOrdered,    // When Customer First Made and Order
        MerchantViewed,     // Order was made, but merchant didnt read this - Will be used when filtering NON-VIEWED Orders for Merchant
        MerchantVerified,   // Merchant checked the order and stock - Order can be delivered
        MerchantRefused,    // Merchant checked the stock and was not sufficient
        OrderPreparing,     // Order is being prepared by the delivery boy
        DeliveryPending,    // Basket is ready at shop
        DeliveryOnTheWay,   // Delivery boys is on his way to customer
        Delivered           // Order delivered and payment made by customer
    }
}
