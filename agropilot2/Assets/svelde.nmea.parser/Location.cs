using System;

namespace svelde.nmea.parser
{
    /// <summary>
    /// Position - location
    /// </summary>
    public class Location
    {
        public Location(string degree)
        {
            Degree = degree;
        }

        public string Degree { get; private set; }

        /// <summary>
        /// XXYY.YYYY = XX + (YYYYYY / 600000) graden.
        /// (d)dd + (mm.mmmm/60) (* -1 for W and S)
        /// </summary>
        /// <returns></returns>
        public decimal ToDecimalDegrees()
        {
            if (string.IsNullOrEmpty(Degree))
            {
                return -1;
            }

            var list = Degree.Split('.');
            
            var minuteMajor = list[0].Substring(list[0].Length - 2);

            var degree = list[0].Substring(0, list[0].Length-2);

            var nesw = list[1].Substring(list[1].Length - 1);

            var minuteMinor = list[1].Substring(0, list[1].Length - 1);

            var minute = minuteMajor + "." + minuteMinor;

            var plusMinus = nesw == "S" || nesw == "W" ? -1 : 1;

            var result = (Convert.ToDecimal(degree) + (Convert.ToDecimal(minute) / 60.0m)) * plusMinus;

            return result;
        }

        public override string ToString()
        {
            return ToDecimalDegrees().ToString("N8");
        }
    }
}

