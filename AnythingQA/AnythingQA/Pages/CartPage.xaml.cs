using AnythingQA.Model;
using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AnythingQA.Pages
{
    public partial class CartPage : ContentPage
    {
        SteamIronManager manager;
        static Cart cart;
        static ObservableCollection<TempCartItem> tempCartItems = new ObservableCollection<TempCartItem>();

        public CartPage()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
            cart = new Cart();
            this.InitCart();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.RefreshCart();
        }

        private void RefreshCart()
        {
            cartItemsList.ItemsSource = tempCartItems;
        }

        private void InitCart()
        {
            cart.Id = Guid.NewGuid().ToString("N");
            cart.Customer = "CUSTOMER GUID FROM SETTINGS";
            cart.Merchant = "MERCHANT GUID FROM NAVIGATION";
        }

        public void AddCartItem(string itemId, string name, int count)
        {
            //CartItem item = new CartItem();
            //item.Id = Guid.NewGuid().ToString("N");
            //item.Product = itemId;
            //item.Count = count;
            //cart.CartItems.Add(item);
            tempCartItems.Add(new TempCartItem { ProductId = itemId, Name = name, Count = count });
        }

        private void UpdateCartItemCount(string itemId, int count)
        {
            tempCartItems.FirstOrDefault(item => item.ProductId == itemId).Count = count;
        }

        public void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // SHOULD BE ASYNC   Not Implemented
        }

        public void OnCartItemRemove(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            TempCartItem selectedCartItem = mi.CommandParameter as TempCartItem;
            tempCartItems.Remove(selectedCartItem);
            this.RefreshCart();
        }

        public async void OnButtonCheckoutClicked(object sender, EventArgs e)
        {
            List<CartItem> cartItems = new List<CartItem>();

            foreach (var item in tempCartItems)
            {
                CartItem cartItem = new CartItem { Id = Guid.NewGuid().ToString("N"), Product = item.ProductId, Count = item.Count };
                cartItems.Add(cartItem);
            }

            var cart = new Cart
            {
                Customer = "f2565d1471724176814cadba368f52fc",
                Merchant = "81c687c3-1365-4498-86db-533540537b71",
                CartItems = cartItems
            };

            await AddCart(cart);
        }

        async Task AddCart(Cart cart)
        {
            await manager.SaveCartAsync(cart);
        }

        async Task AddCartItems(CartItem cartItem)
        { }

        async Task AddOrder(Order order)
        { }
    }

    public class TempCartItem
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
