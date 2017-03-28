using AnythingQA.Pages;
using System;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using AnythingQA.Pages.MerchantPages;

namespace AnythingQA
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckRegistered();
        }

        private void CheckRegistered()
        {
            bool isCustomer = false;
            bool isMerchant = false;
            if (Application.Current.Properties.ContainsKey(Constants.AppDataGeneralUserType))
            {
                isCustomer = Equals(Application.Current.Properties[Constants.AppDataGeneralUserType], Constants.AppDataGeneralCustomerUserType);
                isMerchant = Equals(Application.Current.Properties[Constants.AppDataGeneralUserType], Constants.AppDataGeneralMerchantUserType);
            }
            if (!isCustomer && !isMerchant)
            {
                DisplayAlert("", isCustomer.ToString() + " " + isMerchant.ToString(), "OK");
                Navigation.PushModalAsync(new RegisterCustomer());
            }
            else if (!isCustomer && isMerchant)
            {
                DisplayAlert("", isCustomer.ToString() + " " + isMerchant.ToString(), "OK");
                Navigation.PushModalAsync(new MerchantPage());
            }

            // If customer, continue to this page
            // If both customer and merchant - WHAT HAS TO BE DONE?
        }
        
        void OnButtonGroceryClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ShoppingPage());
        }

        void OnButtonRestaurentClicked(object sender, EventArgs e)
        {
        }

        async void OnEnglishClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Change Language?", "Change Language to English", "Yes", "No");
        }

        async void OnArabicClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Change Language?", "Change Language to Arabic", "Yes", "No");
        }
    }
}
