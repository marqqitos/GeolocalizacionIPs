using GeolocalizacionIPs.DTOs;
using GeolocalizacionIPs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Repositories.Interfaces
{
    public interface IIPInfoRepository
    {
        Task<IPInfo> GetByCodigoPais(string codigoPais);
        Task<IPInfo> Insert(IPInfo ipInfo);
        Task<IPInfo> GetDistanciaABuenosAires(bool masCercana);
        Task<IList<IPInfo>> GetDistanciaPromedio();
        Task UpdateInvocaciones(IPInfo ipInfo);
    }
}
