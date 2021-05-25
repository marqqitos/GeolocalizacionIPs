using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.DTOs.ServicesExternal
{
    public class FixerResponseDTO
    {
        public Dictionary<string, double> Rates { get; set; }
    }
}
