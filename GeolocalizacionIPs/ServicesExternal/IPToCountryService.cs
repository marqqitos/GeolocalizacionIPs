using GeolocalizacionIPs.Constants;
using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.ServicesExternal.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal
{
    public class IPToCountryService : IIPToCountryService
    {
        private readonly HttpClient _httpClient;

        public IPToCountryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(ExternalServicesClientName.IP_TO_COUNTRY_HTTP_CLIENT_NAME);
        }

        public async Task<IPToCountryResponseDTO> GetPais(string IP)
        {
            var url = "ip?" + IP;
            var response = await _httpClient.GetAsync(url);
            
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pais = JsonConvert.DeserializeObject<IPToCountryResponseDTO>(content);
                return pais;
            }
            else
            {
                throw new Exception("Ocurrio un error al intentar obtener el pais por la IP");
            }
        }
    }
}
