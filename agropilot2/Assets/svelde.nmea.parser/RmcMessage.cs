using System;

namespace svelde.nmea.parser
{
    public abstract class RmcMessage : NmeaMessage
    {
  
        public string TimeOfFix { get; private set; }

        public string NavigationReceiverWarning { get; private set; }

        public Location Latitude { get; private set; }

        public Location Longitude { get; private set; }

        public string SpeedOverGround { get; private set; }

        public string CourseMadeGood { get; private set; }

        public string DateOfFix { get; private set; }

        public string MagneticVariation { get; private set; }

        public ModeIndicator ModeIndicator { get; private set; }

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

            TimeOfFix = items[0];

            //A = OK, V = warning
            switch (items[1])
            {
                case "A":
                    NavigationReceiverWarning = "OK";
                    break;

                case "V":
                    NavigationReceiverWarning = "Warning";
                    break;
            }


            Latitude = new Location (items[2]+ items[3]);
            Longitude = new Location(items[4]+ items[5]);
            SpeedOverGround = items[6];
            CourseMadeGood = items[7];
            DateOfFix = items[8];
            MagneticVariation = items[9]+ items[10];

            ModeIndicator = items.Length > 11
                ? new ModeIndicator(items[11])
                : new ModeIndicator("");

            OnNmeaMessageParsed(this);
        }

        protected override void OnNmeaMessageParsed(NmeaMessage e)
        {
            base.OnNmeaMessageParsed(e);
        }

        public override string ToString()
        {
            var result = $"{Type}-{Port} Time:{TimeOfFix} Warning:{NavigationReceiverWarning} Latitude:{Latitude} Longitude:{Longitude} Speed:{SpeedOverGround} Course:{CourseMadeGood} Date:{DateOfFix} Variation:{MagneticVariation} Mode:{ModeIndicator} ";

            return result;
        }
    }
}

