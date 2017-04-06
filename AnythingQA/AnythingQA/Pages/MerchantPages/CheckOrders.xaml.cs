using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnythingQA.Pages.MerchantPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckOrders : ContentPage
    {
        SteamIronManager manager;
        public CheckOrders()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshItems(true, syncItems: true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                orderList.ItemsSource = await manager.GetOrderForMerchantAsync((string)Application.Current.Properties[Constants.AppDataMerchantNameKey], Constants.OrderStatusCustomerOrdered);
            }
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }
    }
}
