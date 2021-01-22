using System;

namespace svelde.nmea.parser
{
    using System.Collections.Generic;
    using System.Linq;

    public class NmeaParser
    {
        private Dictionary<string, NmeaMessage> _parsers;

        public NmeaParser() : this(string.Empty)
        {
        }

        public NmeaParser(string filter)
        {
            UpdateFilter(filter);
        }

        public void UpdateFilter(string filter)
        {
            _parsers = new Dictionary<string, NmeaMessage>();

            var gngga = new GnggaMessage();
            if (!filter.ToUpper().Contains(gngga.Type))
            {
                gngga.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gngga.Type}", gngga);
            }

            var gpgga = new GpggaMessage();
            if (!filter.ToUpper().Contains(gpgga.Type))
            {
                gpgga.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gpgga.Type}", gpgga);
            }

            var gngll = new GngllMessage();
            if (!filter.ToUpper().Contains(gngll.Type))
            {
                gngll.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gngll.Type}", gngll);
            }

            var gngsa = new GngsaMessage();
            if (!filter.ToUpper().Contains(gngsa.Type))
            {
                gngsa.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gngsa.Type}", gngsa);
            }

            var gpgsa = new GpgsaMessage();
            if (!filter.ToUpper().Contains(gpgsa.Type))
            {
                gpgsa.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gpgsa.Type}", gpgsa);
            }

            var gpgsv = new GpgsvMessage();
            if (!filter.ToUpper().Contains(gpgsv.Type))
            {
                gpgsv.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gpgsv.Type}", gpgsv);
            }

            var glgsv = new GlgsvMessage();
            if (!filter.ToUpper().Contains(glgsv.Type))
            {
                glgsv.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${glgsv.Type}", glgsv);
            }

            var gbgsv = new GbgsvMessage();
            if (!filter.ToUpper().Contains(gbgsv.Type))
            {
                gbgsv.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gbgsv.Type}", gbgsv);
            }

            var gnrmc = new GnrmcMessage();
            if (!filter.ToUpper().Contains(gnrmc.Type))
            {
                gnrmc.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gnrmc.Type}", gnrmc);
            }

            var gprmc = new GprmcMessage();
            if (!filter.ToUpper().Contains(gprmc.Type))
            {
                gprmc.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gprmc.Type}", gprmc);
            }

            var gntxt = new GntxtMessage();
            if (!filter.ToUpper().Contains(gntxt.Type))
            {
                gntxt.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gntxt.Type}", gntxt);
            }

            var gnvtg = new GnvtgMessage();
            if (!filter.ToUpper().Contains(gnvtg.Type))
            {
                gnvtg.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gnvtg.Type}", gnvtg);
            }

            var gpvtg = new GpvtgMessage();
            if (!filter.ToUpper().Contains(gpvtg.Type))
            {
                gpvtg.NmeaMessageParsed += messageParsed;
                _parsers.Add($"${gpvtg.Type}", gpvtg);
            }
        }

        private void messageParsed(object sender, NmeaMessage e)
        {
            if (NmeaMessageParsed != null)
            {
                NmeaMessageParsed(this, e);
            }
        }

        public void Parse(string nmeaLine)
        {
            this.Parse(nmeaLine, "nmea", DateTime.UtcNow);
        }

        public void Parse(string nmeaLine, string port, DateTime timestampUtc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nmeaLine))
                {
                    throw new NmeaParseUnknownException();
                }

                if (nmeaLine.Length < 6)
                {
                    throw new NmeaParseUnknownException($"Incoming message '{nmeaLine}' is not nmea, Port {port} at {timestampUtc})");
                }

                if (_parsers.ContainsKey(nmeaLine.Substring(0, 6)))
                {
                    var p = _parsers.First(x => x.Key == nmeaLine.Substring(0, 6)).Value;

                    p.Port = port;
                    p.TimestampUtc = timestampUtc;

                    p.Parse(nmeaLine);
                }
                else
                {
                    Console.WriteLine($"No parser available for {nmeaLine}");
                }
            }
            catch (NmeaParseChecksumException)
            {
                Console.WriteLine($"PARSE EXCEPTION FOR '{nmeaLine}'");
            }
        }

        public event EventHandler<NmeaMessage> NmeaMessageParsed;
    }
}
