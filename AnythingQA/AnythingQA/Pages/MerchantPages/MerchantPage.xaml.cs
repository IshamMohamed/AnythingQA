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
    public partial class MerchantPage : ContentPage
    {
        public MerchantPage()
        {
            InitializeComponent();
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddProduct());
        }

        private void ModifyProduct_Clicked(object sender, EventArgs e)
        { }

        private void RemoveProduct_Clicked(object sender, EventArgs e)
        { }

        private void CheckOrders_Clicked(object sender, EventArgs e)
        {

        }

        private void MyOrderes_Clicked(object sender, EventArgs e)
        { }

        private void EditMyDetails_Clicked(object sender, EventArgs e)
        { }
    }
}
