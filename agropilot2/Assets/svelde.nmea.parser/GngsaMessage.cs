using System;
using System.Linq;

namespace svelde.nmea.parser
{
    public class GngsaMessage : GsaMessage
    {
        public GngsaMessage()
        {
            Type = "GNGSA";
        }

        public override void Parse(string nmeaLine)
        {
            base.Parse(nmeaLine);
        }
    }
}

