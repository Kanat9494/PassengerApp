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
using PassengerApp.Models.DTOs.Responses;
using PassengerApp.Models.DTOs.Requests;
using PassengerApp.Services;

namespace PassengerApp
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mainViewModel;
        Position position;
        List<DriverLocation> getDrivers;
        List<DriverLocation> foundDrivers;
        DriverRequest driverRequest;
        string busNumber;
        private bool isEmpty;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();

            foundDrivers = new List<DriverLocation>();
            getDrivers = new List<DriverLocation>();
        }

        async void FindDriversButton_Clicked(object sender, EventArgs e)
        {
            Indicator.IsRunning = true;
            Indicator.IsEnabled = true;
            getDrivers.Clear();
            busNumber = this.BusNumber.Text;
            position = await mainViewModel.GetUserLocation();
            localMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(2000)));
            driverRequest = new DriverRequest()
            {
                BusNumber = this.BusNumber.Text,
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
            getDrivers = await FindDrivers.Instance.FindNearByDrivers(driverRequest);
            if (getDrivers == null)
            {
                return;
            }

            foreach (var driver in getDrivers)
            {
                
                var foundDriver = foundDrivers.Find(x => x.DriverId == driver.DriverId);
                if (foundDriver != null)
                {
                    //Pin newPin = new Pin() { Position = new Position (foundDriver.Latitude, foundDriver.Longitude ) };
                    foundDriver.Latitude = driver.Latitude;
                    foundDriver.Longitude = driver.Longitude;
                }
                else
                    foundDrivers.Add(driver);
            }

            Indicator.IsEnabled = false;
            Indicator.IsRunning = false;

            foreach (var driver in foundDrivers)
            {
                Pin BusPins = new Pin()
                {
                    Label = busNumber,
                    Type = PinType.Place,
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                    Position = new Position(driver.Latitude, driver.Longitude)
                };
                localMap.Pins.Add(BusPins);
            }
        }

        private void RemovePin(Pin pin)
        {
            localMap.Pins.Remove(pin);
        }
    }
}
