using System;
using System.Collections.Generic;

namespace Topologia
{
    public struct EGB_OFU
    {
        public string ofu;

        public EGB_OFU(string ofu)
        {
            this.ofu = ofu;
            HashSet<string> ofuEnum = new HashSet<string> { 
                "B", "Ba", "Bi", "Bp", "Br", "Bz",
                "E-Ls", "E-Lz", "E-Lzr", "E-N", "E-Ps", "E-R", "E-W", "E-Wp", "E-Ws", "E-Ł",
                "K",
                "Ls", "Lz", "Lzr", 
                "N",
                "Ps",
                "R",
                "S",
                "Ti", "Tk", "Tp", "Tr",
                "W", "Wm", "Wp", "Ws", "Wsr",
                "dr",
                "Ł"
            };
            if (!ofuEnum.Contains(ofu)) Logger.writeWarning("Słownik EGB_OFU nie zawiera wartości: " + ofu);
        }

        public override bool Equals(object obj)
        {
            return ofu.Equals(obj);
        }

        public override string ToString()
        {
            return ofu;
        }

        public override int GetHashCode()
        {
            return ofu.GetHashCode();
        }
    }
}
