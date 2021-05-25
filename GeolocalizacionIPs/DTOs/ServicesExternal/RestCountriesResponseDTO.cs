using Geolocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.DTOs.ServicesExternal
{
    public class RestCountriesResponseDTO
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public IList<double> LatLng { get; set; }
        public IList<string> Timezones { get; set; }
        public IList<RestCountriesCurrencyDTO> Currencies { get; set; }
        public IList<RestCountriesLanguagesDTO> Languages { get; set; }
        public RestCountriesTranslationsDTO Translations { get; set; }

        public class RestCountriesCurrencyDTO
        {
            public string Code { get; set; }
        }

        public class RestCountriesLanguagesDTO
        {
            public string Iso639_1 { get; set; }
            public string NativeName { get; set; }
        }

        public class RestCountriesTranslationsDTO
        {
            public string Es { get; set; }
        }
    }
}
