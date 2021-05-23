using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Services.Interfaces
{
    public interface IDistanciaService
    {
        double GetDistanciaABuenosAiresEnKm(Coordinate coordenadas);
    }
}
