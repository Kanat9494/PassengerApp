using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PassengerApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace PassengerApp
{
    public partial class MainPage : ContentPage
    {
        Position position;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }

        async Task GetUserLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                position = new Position(location.Latitude, location.Longitude);
            }
            catch (Exception ex) { }
        }
    }
}
