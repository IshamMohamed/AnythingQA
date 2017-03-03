using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AnythingQA.Model
{
    public class CartItem
    {
        string id;
        string product;
		int count;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "productId")]
        public string Product
        {
            get { return product; }
            set { product = value; }
        }

		[JsonProperty(PropertyName = "count")]
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
		
        [Version]
        public string Version { get; set; }
    }
}
