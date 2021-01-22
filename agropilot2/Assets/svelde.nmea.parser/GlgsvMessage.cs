using System;

namespace svelde.nmea.parser
{
    public class GlgsvMessage : GsvMessage
    {
        public GlgsvMessage()
        {
            Type = "GLGSV";
        }
        
        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

