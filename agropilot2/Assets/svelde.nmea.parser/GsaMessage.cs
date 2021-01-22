using System;
using System.Collections.Generic;
using System.Linq;

namespace svelde.nmea.parser
{
    public abstract class GsaMessage : NmeaMessage
    {
        public GsaMessage()
        {
            PrnsOfSatellitesUsedForFix = new List<int>();
        }

        public string AutoSelection{ get; private set; }

        public string Fix3D { get; private set; }

        public decimal PercentDop { get; private set; }

        public decimal HorizontalDop { get; private set; }

        public decimal VerticalDop { get; private set; }

        public List<int> PrnsOfSatellitesUsedForFix { get; private set; }

        public override void Parse(string nmeaLine)
        {
            if (PrnsOfSatellitesUsedForFix.Any(x => x <= 32))
            {
                PrnsOfSatellitesUsedForFix.Sort();

                OnNmeaMessageParsed(this);

                PrnsOfSatellitesUsedForFix.Clear();
            }

            if (string.IsNullOrWhiteSpace(nmeaLine) 
                    || !nmeaLine.StartsWith($"${Type}"))
            {
                throw new NmeaParseMismatchException();
            }

            ParseChecksum(nmeaLine);

            if(MandatoryChecksum != ExtractChecksum(nmeaLine))
            {
                throw new NmeaParseChecksumException();
            }

            // remove identifier plus first comma
            var sentence = nmeaLine.Remove(0, $"${Type}".Length+1);

            // remove checksum and star
            sentence = sentence.Remove(sentence.IndexOf('*'));

            var items = sentence.Split(',');

            AutoSelection = items[0];
            Fix3D = items[1];

            AddPrn(items[2]);
            AddPrn(items[3]);
            AddPrn(items[4]);
            AddPrn(items[5]);
            AddPrn(items[6]);
            AddPrn(items[7]);
            AddPrn(items[8]);
            AddPrn(items[9]);
            AddPrn(items[10]);
            AddPrn(items[11]);
            AddPrn(items[12]);
            AddPrn(items[13]);

            PercentDop = Convert.ToDecimal(items[14]);
            HorizontalDop= Convert.ToDecimal(items[15]);
            VerticalDop  = Convert.ToDecimal(items[16]);
        }

        public void AddPrn(string prn)
        {
            if (!string.IsNullOrEmpty(prn))
            PrnsOfSatellitesUsedForFix.Add(Convert.ToInt32(prn));
        }

        public override string ToString()
        {
            var prnsOfSatellitesUsedForFix = string.Empty;

            foreach(var prn in PrnsOfSatellitesUsedForFix)
            {
                prnsOfSatellitesUsedForFix += $"{prn} ";
            }

            prnsOfSatellitesUsedForFix = prnsOfSatellitesUsedForFix.Trim();

            var result = $"{Type}-{Port} AutoSelection:{AutoSelection} Fix3D:{Fix3D} Prns:{prnsOfSatellitesUsedForFix} PDop:{PercentDop:N1} HDop:{HorizontalDop:N1} VDop:{VerticalDop:N1} ";

            return result;
        }

        protected override void OnNmeaMessageParsed(NmeaMessage e)
        {
            base.OnNmeaMessageParsed(e);
        }
    }
}

