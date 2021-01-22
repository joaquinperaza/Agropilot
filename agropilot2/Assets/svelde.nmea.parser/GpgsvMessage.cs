using System;

namespace svelde.nmea.parser
{
    public class GpgsvMessage : GsvMessage
    {
        public GpgsvMessage()
        {
            Type = "GPGSV";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

