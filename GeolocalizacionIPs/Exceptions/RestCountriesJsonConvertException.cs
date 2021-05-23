using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Exceptions
{
    public class RestCountriesJsonConvertException : Exception
    {
        public RestCountriesJsonConvertException()
        {
        }

        public RestCountriesJsonConvertException(string message)
            : base(message)
        {
        }

        public RestCountriesJsonConvertException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
