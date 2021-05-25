using AutoMapper;
using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Repositories.Interfaces;
using GeolocalizacionIPs.Services.Interfaces;
using GeolocalizacionIPs.ServicesExternal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Services
{
    public class IPInfoService : IIPInfoService
    {
        private readonly IMapper _mapper;
        private readonly IIPInfoRepository _ipInfoRepository;
        private readonly IIPToCountryService _ipToCountryService;
        private readonly IRestCountriesService _restCountriesService;
        private readonly IFixerService _fixerService;
        public IPInfoService(IMapper mapper, IIPInfoRepository ipInfoRepository, IIPToCountryService ipToCountryService, IRestCountriesService restCountriesService, IFixerService fixerService)
        {
            _mapper = mapper;
            _ipInfoRepository = ipInfoRepository;
            _ipToCountryService = ipToCountryService;
            _restCountriesService = restCountriesService;
            _fixerService = fixerService;
        }

        public async Task<IPInfoDTO> GetIPInfo(string IP)
        {
            var pais = await _ipToCountryService.GetPais(IP);
            var ipInfo = await _ipInfoRepository.GetByCodigoPais(pais.CountryCode);

            if(ipInfo == null)
            {
                var newIpInfo = await _restCountriesService.GetInformacionPais(pais);
                ipInfo = await _ipInfoRepository.Insert(newIpInfo);
            }
            else
            {
                await _ipInfoRepository.UpdateInvocaciones(ipInfo);
            }

            var ipInfoDto = _mapper.Map<IPInfoDTO>(ipInfo);

            ipInfoDto.CotizacionDolar = await _fixerService.GetCotizacion(ipInfoDto.Moneda);

            return ipInfoDto;
        }

        public async Task<IPDistanciaDTO> GetDistanciaABuenosAires(bool masCercana)
        {
            var info = await _ipInfoRepository.GetDistanciaABuenosAires(masCercana);
            var distanciaDTO = _mapper.Map<IPDistanciaDTO>(info);

            return distanciaDTO;
        }

        public async Task<IPDistanciaDTO> GetDistanciaPromedio()
        {
            var allInfos = await _ipInfoRepository.GetDistanciaPromedio();

            if(allInfos.Count > 0)
            {
                var sumaDistanciaPorInvocaciones = 0d;
                var sumaInvocaciones = 0d;

                foreach (var info in allInfos)
                {
                    sumaDistanciaPorInvocaciones += info.DistanciaABuenosAires * info.Invocaciones;
                    sumaInvocaciones += info.Invocaciones;
                }

                var distanciaPromedio = sumaDistanciaPorInvocaciones / sumaInvocaciones;

                var distanciaDTO = new IPDistanciaDTO()
                {
                    Distancia = distanciaPromedio
                };

                return distanciaDTO;
            }

            return null;
        }
    }
}
