using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AnythingQA.Model
{
    public class Customer
    {
        string id;
        string name;
		    string address;
		    string email;
		    string phone;
		    double points;
		    bool verified;

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

		    [JsonProperty(PropertyName = "address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
		
		    [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
		
		    [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
		
		    [JsonProperty(PropertyName = "points")]
        public double Points
        {
            get { return points; }
            set { points = value; }
        }
		
		    [JsonProperty(PropertyName = "verified")]
        public bool Verified
        {
            get { return verified; }
            set { verified = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
