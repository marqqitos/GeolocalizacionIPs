using GeolocalizacionIPs.Constants;
using GeolocalizacionIPs.DTOs.ServicesExternal;
using GeolocalizacionIPs.Exceptions;
using GeolocalizacionIPs.ServicesExternal.Interfaces;
using GeolocalizacionIPs.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal
{
    public class FixerService : IFixerService
    {
        private readonly HttpClient _httpClient;
        private readonly FixerSettings _settings;

        public FixerService(IHttpClientFactory httpClientFactory, FixerSettings settings)
        {
            _httpClient = httpClientFactory.CreateClient(ExternalServicesClientName.FIXER_HTTP_CLIENT_NAME);
            _settings = settings;
        }

        public async Task<double> GetCotizacion(string divisa)
        {
            var url = "?access_key=" + _settings.ApiKey + "&symbols=USD," + divisa + "&format=1";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var fixerJsonResponse = JsonConvert.DeserializeObject<FixerResponseDTO>(content);

                if(fixerJsonResponse.Rates.ContainsKey(divisa))
                {
                    var euroUsdRate = fixerJsonResponse.Rates.GetValueOrDefault("USD");
                    var euroDivisaRate = fixerJsonResponse.Rates.GetValueOrDefault(divisa);
                    var usdEuroRate = 1 / euroUsdRate;
                    return euroDivisaRate * usdEuroRate;
                }
                else
                {
                    throw new FixerGetRateException("No existe cotizacion de la divisa del pais");
                }
                
            }
            else
            {
                throw new FixerGetRateException("Ocurrio un error al intentar obtener la cotizacion al dolar de la divisa");
            }
        }
    }
}
