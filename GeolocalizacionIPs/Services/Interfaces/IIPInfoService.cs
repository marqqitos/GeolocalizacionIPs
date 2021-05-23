using GeolocalizacionIPs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Services.Interfaces
{
    public interface IIPInfoService
    {
        Task<IPInfoDTO> GetIPInfo(string IP);
        Task<IPDistanciaDTO> GetDistanciaABuenosAires(bool masCercana);
        Task<IPDistanciaDTO> GetDistanciaPromedio();
    }
}
