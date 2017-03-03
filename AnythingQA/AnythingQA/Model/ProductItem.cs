using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AnythingQA.Model
{
    public class ProductItem
    {
        
        string id;
        string name;
		string details;
        double price;
		string image;
		string merchant;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

		[JsonProperty(PropertyName = "details")]
        public string Details
        {
            get { return details; }
            set { details = value; }
        }
		
        [JsonProperty(PropertyName = "price")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
		
		
		[JsonProperty(PropertyName = "image")]
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
		
		[JsonProperty(PropertyName = "merchantId")]
        public string Merchant
        {
            get { return merchant; }
            set { merchant = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
