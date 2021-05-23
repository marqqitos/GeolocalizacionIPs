using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Entities
{
    public class IPInfo
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string CodigoISOPais { get; set; }
        public string FranjasHorarias { get; set; }
        public string Idiomas { get; set; }
        public double DistanciaABuenosAires { get; set; }
        public string Moneda { get; set; }
        public long Invocaciones { get; set; }
    }
}
