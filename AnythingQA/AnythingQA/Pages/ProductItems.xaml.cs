using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AnythingQA.Pages
{
    public partial class ProductItems : ContentPage
    {
        SteamIronManager manager;
        public ProductItems()
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
                productList.ItemsSource = await manager.GetProductItemAsync(syncItems);
            }
        }

        public void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // SHOULD BE ASYNC   Not Implemented
        }

        public void OnComplete(object sender, EventArgs e)
        {
            // SHOULD BE ASYNC
            //var mi = ((MenuItem)sender);
            //var todo = mi.CommandParameter as ProductItem;
            //await CompleteItem(todo);
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
