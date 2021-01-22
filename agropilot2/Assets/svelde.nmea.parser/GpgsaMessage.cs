using System;

namespace svelde.nmea.parser
{
    public class GpgsaMessage : GsaMessage
    {
        public GpgsaMessage()
        {
            Type = "GPGSA";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

