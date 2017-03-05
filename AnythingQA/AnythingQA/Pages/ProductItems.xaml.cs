using AnythingQA.Model;
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
        CartPage cartPage;
        public ProductItems()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;

            cartPage = new CartPage();
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

        public void OnAddToCart(object sender, EventArgs e)
        {
            // SHOULD BE ASYNC
            //var mi = ((MenuItem)sender);
            //var item = mi.CommandParameter as ProductItems;
            //await CompleteItem(todo);
            //var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            var mi = ((MenuItem)sender);
            ProductItem selectedProdItem = mi.CommandParameter as ProductItem;
            cartPage.AddCartItem(selectedProdItem.Id, selectedProdItem.Name, 1, selectedProdItem.Price);
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

        void OnButtonViewCartClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }
    }
}
