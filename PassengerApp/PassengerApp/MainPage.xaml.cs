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
        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();

            foundDrivers = new List<DriverLocation>()
            {
                new DriverLocation()
                {
                    DriverId = 3,
                    Latitude = 36.0034,
                    Longitude = 5.34
                }
            };
            getDrivers = new List<DriverLocation>();
        }

        async void FindDriversButton_Clicked(object sender, EventArgs e)
        {
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
                for (int i = 0; i < foundDrivers.Count; i++)
                {
                    if (foundDrivers[i].DriverId == driver.DriverId)
                    {
                        foundDrivers[i].Latitude = driver.Latitude;
                        foundDrivers[i].Longitude = driver.Longitude;
                    }
                    else
                        foundDrivers.Add(driver);

                    Pin BusePins = new Pin()
                    {
                        Label = busNumber,
                        Type = PinType.Place,
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                    };
                }
                //foreach (var foundDriver in foundDrivers)
                //{
                //    if (foundDriver.DriverId == driver.DriverId)
                //    {
                //        foundDriver.Latitude = driver.Latitude;
                //        foundDriver.Longitude = driver.Longitude;
                //    }
                //    else
                //        foundDrivers.Add(driver);
                //}
            }
        }
    }
}
