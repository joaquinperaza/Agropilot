using System;

namespace svelde.nmea.parser
{
    public class GnggaMessage : NmeaMessage
    {
        public GnggaMessage()
        {
            Type = "GNGGA";
        }

        public string FixTaken { get; private set; }

        public Location Latitude { get; private set; }

        public Location Longitude { get; private set; }

        public string FixQuality { get; private set; }

        public int NumberOfSatellites { get; private set; }

        public decimal HorizontalPod { get; private set; }

        public string AltitudeMetres { get; private set; }

        public string HeightOfGeoid { get; private set; }

        public string SecondsSinceLastUpdateDGPS { get; private set; }

        public string StationIdNumberDGPS { get; private set; }

        public override void Parse(string nmeaLine)
        {
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

            FixTaken = items[0];
            Latitude = new Location(items[1] + items[2]);
            Longitude = new Location(items[3] + items[4]);

            var fixQuality = "Invalid"; // 0 or other values

            switch(items[5])
            {
                case "1":
                    fixQuality = "GPS fix";
                    break;
                case "2":
                    fixQuality = "DGPS fix";
                    break;
            }

            FixQuality = fixQuality;

            NumberOfSatellites  = Convert.ToInt32(items[6]);
            HorizontalPod  = Convert.ToDecimal(items[7]);
            AltitudeMetres = items[8] + items[9];
            HeightOfGeoid = items[10] + items[11];
            SecondsSinceLastUpdateDGPS = items[12];
            StationIdNumberDGPS = items[13];

            OnNmeaMessageParsed(this);
        }

        protected override void OnNmeaMessageParsed(NmeaMessage e)
        {
            base.OnNmeaMessageParsed(e);
        }

        public override string ToString()
        {
            var result = $"{Type}-{Port} Latitude:{Latitude} Longitude:{Longitude} FixTaken:{FixTaken} Quality:{FixQuality} SatCount:{NumberOfSatellites} HDop:{HorizontalPod:N1} Altitude:{AltitudeMetres} Geoid:{HeightOfGeoid} LastUpdate:{SecondsSinceLastUpdateDGPS} DGPS:{StationIdNumberDGPS} ";

            return result;
        }
    }
}

