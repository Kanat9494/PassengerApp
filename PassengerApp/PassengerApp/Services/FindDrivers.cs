using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PassengerApp.Services
{
    internal class FindDrivers
    {
        private static FindDrivers _instance;
        private JsonSerializer _jsonSerializer = new JsonSerializer();
        private HttpClient httpClient;

        public FindDrivers() 
        { 
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.1.51:45455");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
