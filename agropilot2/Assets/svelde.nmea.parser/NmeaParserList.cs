using System;

namespace svelde.nmea.parser
{
    using System.Collections.Generic;
    using System.Linq;
    public class NmeaParserList : Dictionary<string, NmeaParser>
    { 
        private string _filter;

        public NmeaParser Find(string port)
        {
            if (this.ContainsKey(port))
            {
                return this[port];
            }

            var nmeaParser = new NmeaParser(_filter);
            nmeaParser.NmeaMessageParsed += NmeaMessageParsed;

            this.Add(port, nmeaParser);

            Console.WriteLine($"Parser added for port '{port}'");

            return nmeaParser;
        }

        public void UpdateFilter(string filter)
        {
            _filter = filter;

            foreach(var nmeaParser in this.Values)
            {
                nmeaParser.UpdateFilter(_filter);
            }
        }
        
        private void NmeaMessageParsed(object sender, NmeaMessage e)
        {
            if (NmeaMessagesParsed != null)
            {
                NmeaMessagesParsed(sender, e);
            }
        }

        public event EventHandler<NmeaMessage> NmeaMessagesParsed;
    }
}
