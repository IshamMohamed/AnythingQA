using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AnythingQA.Model
{
    public class Cart
    {
        string id;
        string customer;
        string merchant ;
        ICollection<CartItem> cartItems;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "customerId")]
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        [JsonProperty(PropertyName = "merchantId")]
        public string Merchant 
        {
            get { return merchant; }
            set { merchant = value; }
        }
        
        [JsonProperty(PropertyName = "cartItem")]
        public ICollection<CartItem> CartItems
        {
            get { return cartItems; }
            set { cartItems = value; }
        }
        
        [Version]
        public string Version { get; set; }
    }
}
