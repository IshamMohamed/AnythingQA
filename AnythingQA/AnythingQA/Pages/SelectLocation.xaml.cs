using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using AnythingQA.Pages;

namespace AnythingQA
{
    public partial class SelectLocation : ContentPage
    {
        Plugin.Geolocator.Abstractions.Position position;
        public SelectLocation()
        {
            InitializeComponent();
            //PutSomePinsOnMap();
        }

        async void PutSomePinsOnMap()
        {
            //// define a center point and some sample pins
            //Position tourEiffel = new Position(48.859217, 2.293914);
            //Pin[] pins =
            //{
            //    new Pin() {  Label = "Tour Eiffel",
            //        Position = new Position(48.858234, 2.293774), Type = PinType.Place },
            //    new Pin() {  Label = "Concorde",
            //        Position = new Position(48.865475, 2.321142), Type = PinType.Place },
            //    new Pin() {  Label = "Étoile",
            //        Position = new Position(48.873880, 2.295101), Type = PinType.Place },
            //    new Pin() {  Label = "La Défense",
            //        Position = new Position(48.892418, 2.236180), Type = PinType.Place },
            //};

            //foreach (Pin p in pins)
            //{
            //    theMap.Pins.Add(p);
            //}

            //// center the map on Tour Eiffel / set the zoom level
            //theMap.MoveToRegion(MapSpan.FromCenterAndRadius(tourEiffel, Distance.FromKilometers(2.5)));
            try
            {
                var locator = CrossGeolocator.Current;
                if (!locator.IsListening)
                    await locator.StartListeningAsync(100, 1000);
                var x = locator.IsGeolocationAvailable;
                position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            }
            catch (Exception ex)
            {

            }
            Pin myLocationPin = new Pin
            {
                Label = "My Current Location",
                Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                Type = PinType.Place
            };
            theMap.Pins.Add(myLocationPin);
            theMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(0.25)));
        }

        async void OnButtonGetCurrrentLocClicked(object sender, EventArgs e)
        {
            PutSomePinsOnMap();
            Geocoder geocoder = new Geocoder();
            var addresses = await geocoder.GetAddressesForPositionAsync(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
            //foreach (var address in addresses)
            //    txtDeliveryAddress.Text += address + ",";

            txtDeliveryAddress.Text = addresses.FirstOrDefault();
        }
        void OnButtonGoShoppingClicked(object sender, EventArgs e)
        {
            //var xamlTab = new TabbedPage();
            //xamlTab.Children.Add(new ProductItems { Title = "Products" });
            //xamlTab.Children.Add(new CartPage { Title = "Cart" });
            Navigation.PushModalAsync(new ShoppingPage());
        }
    }
}
