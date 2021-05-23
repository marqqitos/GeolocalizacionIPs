using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.DTOs
{
    public class IPInfoDTO
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string CodigoISOPais { get; set; }
        public IList<string> FranjasHorarias { get; set; }
        public IList<string> Idiomas { get; set; }
        public double DistanciaABuenosAires { get; set; }
        public string Moneda { get; set; }
        public double CotizacionDolar { get; set; }
    }
}
