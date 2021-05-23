using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.ServicesExternal.Interfaces
{
    public interface IFixerService
    {
        Task<double> GetCotizacion(string divisa);
    }
}
