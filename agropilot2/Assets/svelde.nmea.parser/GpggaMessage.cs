using System;

namespace svelde.nmea.parser
{
    public class GpggaMessage : GnggaMessage
    {
        public GpggaMessage()
        {
            Type = "GPGGA";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

