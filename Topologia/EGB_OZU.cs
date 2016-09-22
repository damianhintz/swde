using System;
using System.Collections.Generic;

namespace egib.TopologiaKonturuów
{
    public struct EGB_OZU
    {
        public string ozu;

        public EGB_OZU(string ozu)
        {
            this.ozu = ozu;
            HashSet<string> ozuEnum = new HashSet<string> { "Ls", "Lz", "Ps", "R", "Ł" };
            if (!ozuEnum.Contains(ozu)) Logger.writeWarning("Słownik EGB_OZU nie zawiera wartości: " + ozu);
        }

        public override bool Equals(object obj)
        {
            return ozu.Equals(obj);
        }

        public override string ToString()
        {
            return ozu;
        }

        public override int GetHashCode()
        {
            return ozu.GetHashCode();
        }
    }
}
