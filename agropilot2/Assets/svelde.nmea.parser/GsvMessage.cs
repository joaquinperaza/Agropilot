
using System;
using System.Collections.Generic;

namespace svelde.nmea.parser
{
    public abstract class GsvMessage : NmeaMessage
    {
        public GsvMessage()
        {
            Satelites = new List<Satelite>();
        }

        public int NumberOfSentences { get; private set; }

        public int SentenceNr { get; private set; }

        public int NumberOfSatelitesInView { get; private set; }

        public List<Satelite> Satelites { get; private set; }

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

            NumberOfSentences = Convert.ToInt32(items[0]);
            SentenceNr = Convert.ToInt32(items[1]);
            NumberOfSatelitesInView = Convert.ToInt32(items[2]);

            var sateliteCount = GetSateliteCount(
                Convert.ToInt32(NumberOfSatelitesInView),
                Convert.ToInt32(NumberOfSentences),
                Convert.ToInt32(SentenceNr));

            for (int i = 0; i < sateliteCount; i++)
            {
                Satelites.Add(
                    new Satelite
                    {
                        SatelitePrnNumber = items[3 + (i * 4) + 0],
                        ElevationDegrees = items[3 + (i * 4) + 1],
                        AzimuthDegrees = items[3 + (i * 4) + 2],
                        SignalStrength = items[3 + (i * 4) + 3],
                    });
            }

            if (NumberOfSentences == SentenceNr)
            {
                OnNmeaMessageParsed(this);

                Satelites.Clear();
            }
        }

        protected override void OnNmeaMessageParsed(NmeaMessage e)
        {
            base.OnNmeaMessageParsed(e);
        }

        private int GetSateliteCount(int numberOfSatelitesInView, int numberOfSentences, int sentenceNr)
        {
            if (numberOfSentences != sentenceNr)
            {
                return 4;
            }
            else
            {
                return numberOfSatelitesInView - ((sentenceNr - 1) * 4);
            }
        }

        public override string ToString()
        {
            var result = $"{Type}-{Port} InView:{NumberOfSatelitesInView} ";

            foreach(var s in Satelites)
            {
                result += $"{s.SatelitePrnNumber}: Azi={s.AzimuthDegrees}° Ele={s.ElevationDegrees}° Str={s.SignalStrength}; ";
            }

            return result; 
        }
    }
}

