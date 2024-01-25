using System;
using CapstoneUI.Data;
using Newtonsoft.Json;

namespace CapstoneUI.Services
{
    public class RainFallService
    {
        private readonly HttpClient httpClient;

        public RainFallService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<RainfallData>> GetAll()
        {
            using HttpResponseMessage response =  await httpClient.GetAsync("https://localhost:7019/weatherforecast/getall");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<RainfallData>>(jsonResponse) ?? new List<RainfallData>();
        }
    }
}
