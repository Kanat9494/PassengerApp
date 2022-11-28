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
using System.Threading;

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
        Pin BusPins;
        private Timer timer;
        private bool isSearching = false;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();

            foundDrivers = new List<DriverLocation>();
            getDrivers = new List<DriverLocation>();
        }

        async void FindDriversButton_Clicked(object sender, EventArgs e)
        {
            isSearching = true;
            Indicator.IsRunning = true;
            Indicator.IsEnabled = true;
            getDrivers.Clear();
            busNumber = this.BusNumber.Text;
            position = await mainViewModel.GetUserLocation();
            Position localPosition = new Position(42.853070, 74.526750);
            //Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
            Pin localPin = new Pin()
            {
                Label = "Ваше местоположение",
                Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("PickupPin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "PickupPin.png", WidthRequest = 30, HeightRequest = 30 }),
                Position = new Position(42.853070, 74.526750)
            };
            localMap.Pins.Add(localPin);
            localMap.MoveToRegion(MapSpan.FromCenterAndRadius(localPosition, Distance.FromMeters(2000)));


            driverRequest = new DriverRequest()
            {
                BusNumber = this.BusNumber.Text,
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };

            //getDrivers = await FindDrivers.Instance.FindNearByDrivers(driverRequest);
            //if (getDrivers == null)
            //{
            //    return;
            //}

            Indicator.IsEnabled = false;
            Indicator.IsRunning = false;

            localMap.Pins.Remove(BusPins);

            
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (isSearching)
                {
                    Task.Factory.StartNew(async () =>
                    {
                        getDrivers = await FindDrivers.Instance.FindNearByDrivers(driverRequest);
                        if (getDrivers == null)
                        {
                            await DisplayAlert("Поиск остановлен", "Поиск маршруток был остановлен", "Ок");
                            return;
                        }

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            localMap.Pins.Clear();
                            foreach (var driver in getDrivers)
                            {
                                BusPins = new Pin()
                                {
                                    Label = busNumber,
                                    Type = PinType.Place,
                                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                                    Position = new Position(driver.Latitude, driver.Longitude)
                                };
                                localMap.Pins.Add(BusPins);
                            }
                        });
                    });
                    return true;
                }
                else
                    return false;
            });
            
            //timer = new Timer(async (object stateInfo) =>
            //{
            //    getDrivers = await FindDrivers.Instance.FindNearByDrivers(driverRequest);
            //    if (getDrivers == null)
            //    {
            //        return;
            //    }

            //    //foreach (var driver in getDrivers)
            //    //{

            //    //    var foundDriver = foundDrivers.Find(x => x.DriverId == driver.DriverId);
            //    //    if (foundDriver != null)
            //    //    {
            //    //        //Pin newPin = new Pin() { Position = new Position (foundDriver.Latitude, foundDriver.Longitude ) };
            //    //        foundDriver.Latitude = driver.Latitude;
            //    //        foundDriver.Longitude = driver.Longitude;
            //    //    }
            //    //    else
            //    //        foundDrivers.Add(driver);
            //    //}

            //    Indicator.IsEnabled = false;
            //    Indicator.IsRunning = false;

            //    localMap.Pins.Remove(BusPins);

            //    //foreach (var driver in getDrivers)
            //    //{
            //    //    BusPins = new Pin()
            //    //    {
            //    //        Label = busNumber,
            //    //        Type = PinType.Place,
            //    //        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
            //    //        Position = new Position(driver.Latitude, driver.Longitude)
            //    //    };
            //    //    localMap.Pins.Add(BusPins);
            //    //}
            //}, new AutoResetEvent(false), 4000, 4000);

            //getDrivers = await FindDrivers.Instance.FindNearByDrivers(driverRequest);
            //if (getDrivers == null)
            //{
            //    return;
            //}

            //foreach (var driver in getDrivers)
            //{

            //    var foundDriver = foundDrivers.Find(x => x.DriverId == driver.DriverId);
            //    if (foundDriver != null)
            //    {
            //        //Pin newPin = new Pin() { Position = new Position (foundDriver.Latitude, foundDriver.Longitude ) };
            //        foundDriver.Latitude = driver.Latitude;
            //        foundDriver.Longitude = driver.Longitude;
            //    }
            //    else
            //        foundDrivers.Add(driver);
            //}

            //Indicator.IsEnabled = false;
            //Indicator.IsRunning = false;

            //localMap.Pins.Remove(BusPins);

            //foreach (var driver in getDrivers)
            //{
            //    BusPins = new Pin()
            //    {
            //        Label = busNumber,
            //        Type = PinType.Place,
            //        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
            //        Position = new Position(driver.Latitude, driver.Longitude)
            //    };
            //    localMap.Pins.Add(BusPins);
            //}
        }

        void CancelSearchingButton_Clicked(object sender, EventArgs e)
        {
            isSearching = false;
            localMap.Pins.Clear();
            Position localPosition = new Position(42.853070, 74.526750);

            Pin localPin = new Pin()
            {
                Label = "Ваше местоположение",
                Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("PickupPin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "PickupPin.png", WidthRequest = 30, HeightRequest = 30 }),
                Position = new Position(42.853070, 74.526750)
            };
            localMap.Pins.Add(localPin);
            localMap.MoveToRegion(MapSpan.FromCenterAndRadius(localPosition, Distance.FromMeters(2000)));
        }

    }
}
