using System;

namespace svelde.nmea.parser
{
    public class GnrmcMessage : RmcMessage
    {
        public GnrmcMessage()
        {
            Type = "GNRMC";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

