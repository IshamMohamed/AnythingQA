using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnythingQA.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnythingQA.Pages.MerchantPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterMerchant : ContentPage
    {
        bool authenticated = false;
        SteamIronManager manager;
        public RegisterMerchant()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
            btnRegister.IsEnabled = false;
        }

        private async void AthencicateUser_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.Authenticate();
            }

            // Send SMS Code On Successfull authentication
            bool smsVerified = string.Equals("1234", txtMerchSMSCode.Text);

            btnRegister.IsEnabled = authenticated && smsVerified ? true : false;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMerchName.Text)
                && !string.IsNullOrEmpty(txtMerchAddress.Text)
                && !string.IsNullOrEmpty(txtMerchEmail.Text)
                && !string.IsNullOrEmpty(txtMerchPhoneNo.Text)
                && !string.IsNullOrEmpty(txtMerchSMSCode.Text))
            {
                var merchant = new Merchant
                {
                    Name = txtMerchName.Text,
                    Address = txtMerchAddress.Text,
                    Phone = txtMerchPhoneNo.Text,
                    Email = txtMerchEmail.Text,
                };
                await RegisterMerchantToSystem(merchant);
                AddMerchantDataToAppProps(merchant);
                if (!Application.Current.Properties.ContainsKey(Constants.AppDataGeneralRegisteredKey))
                    Application.Current.Properties.Add(Constants.AppDataGeneralRegisteredKey, true);
                if (!Application.Current.Properties.ContainsKey(Constants.AppDataGeneralUserType))
                    Application.Current.Properties.Add(Constants.AppDataGeneralUserType, Constants.AppDataGeneralMerchantUserType);
                await DisplayAlert("", "Successfully Registered.", "Continue Shopping");
                await Navigation.PushModalAsync(new MerchantPage());
            }
        }

        async Task RegisterMerchantToSystem(Merchant merchant)
        {
            await manager.SaveMerchantAsync(merchant);
        }

        private void AddMerchantDataToAppProps(Merchant merchant)
        {
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataMerchantNameKey))
                Application.Current.Properties.Add(Constants.AppDataMerchantNameKey, merchant.Name);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataMerchantPhoneNoKey))
                Application.Current.Properties.Add(Constants.AppDataMerchantPhoneNoKey, merchant.Phone);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataMerchantEmailKey))
                Application.Current.Properties.Add(Constants.AppDataMerchantEmailKey, merchant.Email);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataMerchantIdKey))
                Application.Current.Properties.Add(Constants.AppDataMerchantIdKey, merchant.Id);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataMerchantAddressKey))
                Application.Current.Properties.Add(Constants.AppDataMerchantAddressKey, merchant.Address);
        }
    }
}
