using PassengerApp.Models.DTOs.Requests;
using PassengerApp.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace PassengerApp.ViewModels
{
    internal class MainViewModel
    {
        Position position;
        async Task<Position> GetUserLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                position = new Position(location.Latitude, location.Longitude);
                return position;
            }
            catch (Exception ex) { return position; }
        }
    }
}
