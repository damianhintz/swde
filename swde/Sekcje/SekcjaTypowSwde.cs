using System.Collections.Generic;

using egib.swde.Rekordy;

namespace egib.swde.Sekcje
{
    internal class SekcjaTypowSwde : SekcjaSwdeBase
    {
        private List<DefinicjaTypuSwde> _typy;

        public SekcjaTypowSwde()
        {
            _typy = new List<DefinicjaTypuSwde>();
        }

        /// <summary>
        /// Dodaj nowy typ.
        /// </summary>
        /// <param name="komponent"></param>
        public override void DodajDefinicjaTypu(DefinicjaTypuSwde komponent)
        {
            _typy.Add(komponent);
        }
    }
}
