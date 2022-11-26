using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PassengerApp.Services
{
    internal class GetNearByDrivers
    {
        private static GetNearByDrivers _instance;
        private JsonSerializer _jsonSerializer = new JsonSerializer();
        private HttpClient httpClient;

        public GetNearByDrivers() 
        { 
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.1.51:45455");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
