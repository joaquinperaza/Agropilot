using System.Collections.Generic;

namespace svelde.nmea.parser
{
    public class ModeIndicator
    {
        private Dictionary<char, string> _modeIndicators;

        public ModeIndicator(string modeIndicator)
        {
            _modeIndicators = new Dictionary<char, string>();
            _modeIndicators.Add('A', "Autonomous");
            _modeIndicators.Add('D', "Differential");
            _modeIndicators.Add('E', "Estimated(dead reckoning) mode");
            _modeIndicators.Add('M', "Manual input");
            _modeIndicators.Add('N', "Data not valid"); 
            _modeIndicators.Add('*', "Not implemented");


            if (!string.IsNullOrEmpty(modeIndicator) 
                && _modeIndicators.ContainsKey(modeIndicator[0]))
            {
                Mode = _modeIndicators[modeIndicator[0]];
            }
            else
            {
                Mode = _modeIndicators['*'];
            }
        }

        public bool IsValid()
        {
            return Mode != _modeIndicators['N'];
        }

        public string Mode { get; private set; }

        public override string ToString()
        {
            return Mode;
        }
    }
}

