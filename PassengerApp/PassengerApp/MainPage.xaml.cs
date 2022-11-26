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

namespace PassengerApp
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mainViewModel;
        Position position;
        IEnumerable<DriverLocation> getDrivers;
        IEnumerable<DriverLocation> foundDrivers;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();
        }
    }
}
