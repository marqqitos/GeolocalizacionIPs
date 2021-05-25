using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.DTOs.ServicesExternal
{
    public class IPToCountryResponseDTO
    {
        public string CountryCode { get; set; }
        public string CountryCode3 { get; set; }
        public string CountryName { get; set; }
    }
}
