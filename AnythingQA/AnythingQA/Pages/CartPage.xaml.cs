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

        public void AddCartItem(string itemId, string name, int count, double price)
        {
            tempCartItems.Add(new TempCartItem { ProductId = itemId, Name = name, Count = count, Price = price });
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
            double totalCost = 0;

            foreach (var item in tempCartItems)
            {
                CartItem cartItem = new CartItem { Id = Guid.NewGuid().ToString("N"), Product = item.ProductId, Count = item.Count };
                cartItems.Add(cartItem);

                // Price Calculation
                totalCost += item.Price*item.Count;
            }

            var cart = new Cart
            {
                Customer = "f2565d1471724176814cadba368f52fc", // FETCH FROM SETTINGS
                Merchant = "81c687c3-1365-4498-86db-533540537b71", // FETCH FROM NAVIGATION
                CartItems = cartItems
            };
            await AddCart(cart);

            var order = new Order { Cart = cart.Id, DeliveryAddress = "GetFromCart", Amount = totalCost, OrderStatus = OrderStatus.CustomerOrdered };
            await AddOrder(order);
        }

        async Task AddCart(Cart cart)
        {
            await manager.SaveCartAsync(cart);
        }

        async Task AddOrder(Order order)
        {
            await manager.SaveOrderAsync(order);
        }
    }

    public class TempCartItem
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
