using AnythingQA.Model;
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
    public partial class AddProduct : ContentPage
    {
        SteamIronManager manager;
        public AddProduct()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
        }

        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductName.Text) &&
               !string.IsNullOrEmpty(txtProductDetails.Text) &&
               !string.IsNullOrEmpty(txtProductPrice.Text))
            {
                ProductItem prod = new ProductItem
                {
                    Name = txtProductName.Text,
                    Details = txtProductDetails.Text,
                    Price = Convert.ToDouble(txtProductPrice),
                    Merchant = (string)Application.Current.Properties[Constants.AppDataMerchantIdKey],
                    Image = ""
                };
                await AddProductToTheSystem(prod);
                await DisplayAlert("", "Product Added Successfully", "OK");
            }
        }

        async Task AddProductToTheSystem(ProductItem prod)
        {
            await manager.SaveProductItemAsync(prod);
        }
    }
}
