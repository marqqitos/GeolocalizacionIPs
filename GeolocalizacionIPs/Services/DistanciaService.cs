using GeolocalizacionIPs.Services.Interfaces;
using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Services
{
    public class DistanciaService : IDistanciaService
    {
        private Coordinate _coordenadasBuenosAires;

        public DistanciaService()
        {
            _coordenadasBuenosAires = new Coordinate();
            _coordenadasBuenosAires.Latitude = -34.6156625;
            _coordenadasBuenosAires.Longitude = -58.503338;
        }
        public double GetDistanciaABuenosAiresEnKm(Coordinate coordenadas)
        {
            return GeoCalculator.GetDistance(_coordenadasBuenosAires, coordenadas, 0, DistanceUnit.Kilometers);
        }
    }
}
