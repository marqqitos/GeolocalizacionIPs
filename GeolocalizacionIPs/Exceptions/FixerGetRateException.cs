using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeolocalizacionIPs.Exceptions
{
    public class FixerGetRateException : Exception
    {
        public FixerGetRateException()
        {
        }

        public FixerGetRateException(string message)
            : base(message)
        {
        }

        public FixerGetRateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
