using System;

namespace svelde.nmea.parser
{
    /// <summary>
    /// Base message
    /// </summary>
    public abstract class NmeaMessage
    {
        public string Type {get; set;}

        public string Port { get; set; }

        public DateTime TimestampUtc { get; set; }

        public string MandatoryChecksum { get; set; }

        /// <summary>
        /// Calculate checksum of Nmea sentence.
        /// </summary>
        /// <param name="sentence">The Nmea sentence</param>
        /// <returns>The hexidecimal checksum</returns>
        /// <remarks>
        /// Example taken from https://gist.github.com/maxp/1193206
        /// </remarks>
        public void ParseChecksum(string sentence)
        {
            //Start with first Item
            int checksum = Convert.ToByte(sentence[sentence.IndexOf('$') + 1]);

            // Loop through all chars to get a checksum
            for (int i = sentence.IndexOf('$') + 2; i < sentence.IndexOf('*'); i++)
            {
                // No. XOR the checksum with this character's value
                checksum ^= Convert.ToByte(sentence[i]);
            }

            // Return the checksum formatted as a two-character hexadecimal
            MandatoryChecksum = checksum.ToString("X2");
        }

        /// <summary>
        /// Take the last characters which should be the checksum
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public string ExtractChecksum(string sentence)
        {
            var index = sentence.LastIndexOf('*');
            if (index == -1)
            {
                return string.Empty;
            }

            return sentence.Substring(index+1);
        }

        public abstract void Parse(string nmeaLine);

        public event EventHandler<NmeaMessage> NmeaMessageParsed;

        protected virtual void OnNmeaMessageParsed(NmeaMessage e)
        {
            if (NmeaMessageParsed != null)
            {
                NmeaMessageParsed(this, e);
            }
        }
    }
}