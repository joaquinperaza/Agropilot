using System;

namespace svelde.nmea.parser
{
    public class NmeaParseUnknownException : Exception
    {
        public NmeaParseUnknownException()
        {
        }

        public NmeaParseUnknownException(string message) : base(message)
        {
        }
    }
}

