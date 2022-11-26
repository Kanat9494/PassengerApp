using System;
using System.Collections.Generic;
using System.Text;

namespace PassengerApp.Models.DTOs.Responses
{
    public class DriverLocationResponse
    {
        public int DriverId { get; set; }   
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
