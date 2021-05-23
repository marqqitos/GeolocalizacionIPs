using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal.Interfaces
{
    public interface IRestCountriesService
    {
        Task<IPInfo> GetInformacionPais(IPToCountryResponseDTO pais);
    }
}
