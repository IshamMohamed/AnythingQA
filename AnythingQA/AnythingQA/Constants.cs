using AnythingQA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnythingQA
{
    public static class Constants
    {
        public static string ApplicationURL = @"https://steamiron.azurewebsites.net/";

        // GeneralAppDataKeys
        public static string AppDataGeneralRegisteredKey = "registered";
        public static string AppDataGeneralUserType = "user_type";
        public static string AppDataGeneralCustomerUserType = typeof(Customer).FullName;
        public static string AppDataGeneralMerchantUserType = typeof(Merchant).FullName;


        // CustomerAppDataKeys
        public static string AppDataCustomerNameKey = "cust_name";
        public static string AppDataCustomerIdKey = "cust_id";
        public static string AppDataCustomerAddressKey = "cust_address";
        public static string AppDataCustomerPhoneNoKey = "cust_phone";
        public static string AppDataCustomerPointsKey = "cust_points";
        public static string AppDataCustomerEmailKey = "cust_email";

        // MerchantAppDataKeys
        public static string AppDataMerchantNameKey = "merch_name";
        public static string AppDataMerchantIdKey = "merch_id";
        public static string AppDataMerchantAddressKey = "merch_address";
        public static string AppDataMerchantPhoneNoKey = "merch_phone";
        public static string AppDataMerchantEmailKey = "merch_email";

        // OrderStatus
        public static string OrderStatusCustomerOrdered = "CustomerOrdered";
        public static string OrderStatusMerchantViewed = "MerchantViewed";
        public static string OrderStatusMerchantVerified = "MerchantVerified";
        public static string OrderStatusMerchantRefused = "MerchantRefused";
        public static string OrderStatusOrderPreparing = "OrderPreparing";
        public static string OrderStatusDeliveryPending = "DeliveryPending";
        public static string OrderStatusDeliveryOnTheWay = "DeliveryOnTheWay";
        public static string OrderStatusDelivered = "Delivered";
    }
}
