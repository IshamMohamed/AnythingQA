using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnythingQA.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnythingQA.Pages.MerchantPages;

namespace AnythingQA.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCustomer : ContentPage
    {
        bool authenticated = false;
        SteamIronManager manager;
        public RegisterCustomer()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
            btnRegister.IsEnabled = false;
            //BindingContext = new ContentPageViewModel();
        }

        private async void AthencicateUser_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.Authenticate();
            }

            // Send SMS Code On Successfull authentication
            bool smsVerified = string.Equals("1234", txtCustSMSCode.Text);

            btnRegister.IsEnabled = authenticated && smsVerified ? true : false;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustName.Text)
                && !string.IsNullOrEmpty(txtCustAddress.Text)
                && !string.IsNullOrEmpty(txtCustEmail.Text)
                && !string.IsNullOrEmpty(txtCustPhoneNo.Text)
                && !string.IsNullOrEmpty(txtCustSMSCode.Text))
            {
                var customer = new Customer
                {
                    Name = txtCustName.Text,
                    Address = txtCustAddress.Text,
                    Phone = txtCustPhoneNo.Text,
                    Email = txtCustEmail.Text,
                    Points = 0,
                    Verified = true
                };
                await RegisterCustomerToSystem(customer);
                AddCustomerDataToAppProps(customer);
                if (!Application.Current.Properties.ContainsKey(Constants.AppDataGeneralRegisteredKey))
                    Application.Current.Properties.Add(Constants.AppDataGeneralRegisteredKey, true);
                if (!Application.Current.Properties.ContainsKey(Constants.AppDataGeneralUserType))
                    Application.Current.Properties.Add(Constants.AppDataGeneralUserType, Constants.AppDataGeneralCustomerUserType);
                await DisplayAlert("", "Successfully Registered.", "Continue Shopping");
                await Navigation.PushModalAsync(new MainPage());
            }
        }

        async Task RegisterCustomerToSystem(Customer customer)
        {
            await manager.SaveCustomerAsync(customer);
        }

        private void AddCustomerDataToAppProps(Customer customer)
        {
            if(!Application.Current.Properties.ContainsKey(Constants.AppDataCustomerPhoneNoKey))
                Application.Current.Properties.Add(Constants.AppDataCustomerPhoneNoKey, customer.Phone);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataCustomerEmailKey))
                Application.Current.Properties.Add(Constants.AppDataCustomerEmailKey, customer.Email);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataCustomerIdKey))
                Application.Current.Properties.Add(Constants.AppDataCustomerIdKey, customer.Id);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataCustomerAddressKey))
                Application.Current.Properties.Add(Constants.AppDataCustomerAddressKey, customer.Address);
            if (!Application.Current.Properties.ContainsKey(Constants.AppDataCustomerPointsKey))
                Application.Current.Properties.Add(Constants.AppDataCustomerPointsKey, customer.Points);
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new RegisterMerchant());
        }
    }


    // USELESS
    class RegisterCustomerViewModel : INotifyPropertyChanged
    {

        public RegisterCustomerViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
