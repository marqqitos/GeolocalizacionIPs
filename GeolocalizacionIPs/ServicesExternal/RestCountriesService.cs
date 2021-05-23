using AutoMapper;
using GeolocalizacionIPs.Constants;
using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Entities;
using GeolocalizacionIPs.Exceptions;
using GeolocalizacionIPs.Services.Interfaces;
using GeolocalizacionIPs.ServicesExternal.Interfaces;
using Geolocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IDistanciaService _distanciaService;

        public RestCountriesService(IHttpClientFactory httpClientFactory, IMapper mapper, IDistanciaService distanciaService)
        {
            _httpClient = httpClientFactory.CreateClient(ExternalServicesClientName.REST_COUNTRIES_HTTP_CLIENT_NAME);
            _mapper = mapper;
            _distanciaService = distanciaService;
        }

        public async Task<IPInfo> GetInformacionPais(IPToCountryResponseDTO pais)
        {
            var url = pais.CountryName;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var restCountriesPaisInfo = new RestCountriesResponseDTO();
                try
                {
                    var jsonResponse = JsonConvert.DeserializeObject<List<RestCountriesResponseDTO>>(content);
                    restCountriesPaisInfo = jsonResponse.Where(p => p.Alpha2Code.Equals(pais.CountryCode)).FirstOrDefault();
                }
                catch(Exception e)
                {
                    throw new RestCountriesJsonConvertException("Ocurrio un error al intentar convertir la respuesta del servicio RestCountries.");
                }

                var ipInfo = _mapper.Map<IPInfo>(restCountriesPaisInfo);

                var coordenadas = new Coordinate();
                coordenadas.Latitude = restCountriesPaisInfo.LatLng[0];
                coordenadas.Longitude = restCountriesPaisInfo.LatLng[1];

                ipInfo.DistanciaABuenosAires = _distanciaService.GetDistanciaABuenosAiresEnKm(coordenadas);
                ipInfo.Invocaciones = 0;

                return ipInfo;
            }
            else
            {
                throw new Exception("Ocurrio un error al intentar obtener el pais por la IP");
            }
        }
    }
}
