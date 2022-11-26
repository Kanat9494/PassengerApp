using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PassengerApp.Models.DTOs.Requests;
using PassengerApp.Models.DTOs.Responses;

namespace PassengerApp.Services
{
    internal class FindDrivers
    {
        private static FindDrivers _instance;
        private JsonSerializer _jsonSerializer = new JsonSerializer();
        private HttpClient httpClient;
        IEnumerable<DriverLocation> _drivers;

        public FindDrivers() 
        { 
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.1.51:45455");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<DriverLocation>> FindNearByDrivers(DriverRequest driverRequest)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(driverRequest), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Driver/GetNearByDrivers", content);

                if (response.IsSuccessStatusCode || response != null)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    _drivers = JsonConvert.DeserializeObject<IEnumerable<DriverLocation>>(jsonResult);
                    return _drivers;
                }
                return null;
            }
            catch (Exception ex) { return null; }
        }
    }
}
