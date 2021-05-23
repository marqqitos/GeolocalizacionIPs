using GeolocalizacionIPs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal.Interfaces
{
    public interface IIPToCountryService
    {
        Task<IPToCountryResponseDTO> GetPais(string IP);
    }
}
