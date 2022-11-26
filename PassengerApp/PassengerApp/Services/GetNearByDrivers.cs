using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PassengerApp.Services
{
    internal class GetNearByDrivers
    {
        private static GetNearByDrivers _instance;
        private JsonSerializer _jsonSerializer = new JsonSerializer();
        public GetNearByDrivers() 
        { 

        }
    }
}
