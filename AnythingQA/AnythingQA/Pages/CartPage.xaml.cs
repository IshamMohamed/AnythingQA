﻿using AnythingQA.Model;
using AnythingQA.ModelManagers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AnythingQA.Pages
{
    public partial class CartPage : ContentPage
    {
        SteamIronManager manager;
        static Cart cart;
        static ObservableCollection<TempCartItem> tempCartItems = new ObservableCollection<TempCartItem>();
        string customerId;
        string merchantId;

        public CartPage()
        {
            InitializeComponent();
            manager = SteamIronManager.DefaultManager;
            cart = new Cart();
            if (Application.Current.Properties.ContainsKey(Constants.AppDataCustomerIdKey))
                customerId = (string)Application.Current.Properties[Constants.AppDataCustomerIdKey];
            merchantId = "81c687c3-1365-4498-86db-533540537b71"; // FETCH FROM NAVIGATION
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
            var isDefaultLocationAnswer = await DisplayAlert("", "Ship to", "Default Location", "Current Location");
            string deliveryAddress = null;
            Cart cart = null;
            double totalCost = 0;
            List<CartItem> cartItems = new List<CartItem>();

            // Cart Creation
            foreach (var item in tempCartItems)
            {
                CartItem cartItem = new CartItem { Id = Guid.NewGuid().ToString("N"), Product = item.ProductId, Count = item.Count };
                cartItems.Add(cartItem);

                // Price Calculation
                totalCost += item.Price * item.Count;
            }

            cart = new Cart
            {
                Customer = customerId,
                Merchant = merchantId,
                CartItems = cartItems
            };

            if (isDefaultLocationAnswer)
            {
                deliveryAddress = (string)Application.Current.Properties[Constants.AppDataCustomerAddressKey];
                await AddCart(cart);

                // Order Creation
                var order = new Order
                {
                    Cart = cart.Id,
                    DeliveryAddress = deliveryAddress,
                    Amount = totalCost,
                    OrderStatus = OrderStatus.CustomerOrdered
                };
                await AddOrder(order);

                await DisplayAlert("", "Successfully Order has been made", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new SelectLocation(cart, totalCost));
            }
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
