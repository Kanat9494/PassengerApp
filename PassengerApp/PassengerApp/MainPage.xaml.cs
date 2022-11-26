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
        MainViewModel mainViewModel;
        Position position;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = mainViewModel = new MainViewModel();
        }
    }
}
