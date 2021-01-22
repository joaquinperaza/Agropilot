using System;


namespace svelde.nmea.parser
{
    public class GntxtMessage : NmeaMessage
    {
        public GntxtMessage()
        {
            Type = "GNTXT";
        }

        public string Text { get; private set; }

        public override void Parse(string nmeaLine)
        {
            if (string.IsNullOrWhiteSpace(nmeaLine)
                    || !nmeaLine.StartsWith($"${Type}"))
            {
                throw new NmeaParseMismatchException();
            }

            ParseChecksum(nmeaLine);

            if (MandatoryChecksum != ExtractChecksum(nmeaLine))
            {
                throw new NmeaParseChecksumException();
            }

            // remove identifier plus first comma
            var sentence = nmeaLine.Remove(0, $"${Type}".Length + 1);

            // remove checksum and star
            sentence = sentence.Remove(sentence.IndexOf('*'));

            var items = sentence.Split(',');

            Text = items[3];

            OnNmeaMessageParsed(this);
        }

        protected override void OnNmeaMessageParsed(NmeaMessage e)
        {
            base.OnNmeaMessageParsed(e);
        }

        public override string ToString()
        {
            var result = $"{Type}-{Port} Text:{Text} ";

            return result;
        }
    }
}

