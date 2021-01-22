using System;

namespace svelde.nmea.parser
{
    public class GpvtgMessage : GnvtgMessage
    {
        public GpvtgMessage()
        {
            Type = "GPVTG";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

