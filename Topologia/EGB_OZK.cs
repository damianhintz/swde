using System;
using System.Collections.Generic;

namespace Topologia
{
    public struct EGB_OZK
    {
        public string ozk;

        public EGB_OZK(string ozk)
        {
            this.ozk = ozk;
            HashSet<string> ozkEnum = new HashSet<string> { "I", "II", "III", "IIIa", "IIIb", "IV", "IVa", "IVb", "V", "VI", "VIz" };
            if (!ozkEnum.Contains(ozk)) Logger.writeWarning("Słownik EGB_OZK nie zawiera wartości: " + ozk);
        }

        public override bool Equals(object obj)
        {
            return ozk.Equals(obj);
        }

        public override string ToString()
        {
            return ozk;
        }

        public override int GetHashCode()
        {
            return ozk.GetHashCode();
        }
    }
}
