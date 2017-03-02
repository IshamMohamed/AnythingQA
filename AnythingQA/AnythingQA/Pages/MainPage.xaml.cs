using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnythingQA
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonGroceryClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SelectLocation());
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
