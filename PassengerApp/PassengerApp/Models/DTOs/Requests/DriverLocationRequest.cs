using System;
using System.Collections.Generic;
using System.Text;

namespace PassengerApp.Models.DTOs.Requests
{
    public class DriverLocationRequest
    {
        public string BusNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
