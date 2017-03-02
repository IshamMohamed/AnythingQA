using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AnythingQA.Model
{
    public class ProductItem
    {
        
        string id;
        string name;
        double price;

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

        [JsonProperty(PropertyName = "price")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
