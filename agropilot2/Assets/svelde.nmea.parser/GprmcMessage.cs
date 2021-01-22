using System;

namespace svelde.nmea.parser
{
    public class GprmcMessage : RmcMessage
    {
        public GprmcMessage()
        {
            Type = "GPRMC";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

