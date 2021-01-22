using System;

namespace svelde.nmea.parser
{
    public class GbgsvMessage : GsvMessage
    {
        public GbgsvMessage()
        {
            Type = "GBGSV";
        }
        
        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

