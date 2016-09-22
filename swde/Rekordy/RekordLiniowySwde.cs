using System.Collections.Generic;

using egib.swde.Komponenty;

namespace egib.swde.Rekordy
{
    /// <summary>
    /// Opis przestrzenny rekordu jest zbiorem polilinii.
    /// </summary>
    internal class RekordLiniowySwde : RekordSwdeBase
    {
        public override string TypBazowy
        {
            get
            {
                return "RL";
            }
        }

        private List<RekordLiniaSwde> _linie;

        public IEnumerable<RekordLiniaSwde> Linie { get { return _linie; } }

        public RekordLiniowySwde(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
            _linie = new List<RekordLiniaSwde>();
        }

        public override RekordLiniaSwde DodajLiniaLubObszar()
        {
            RekordLiniaSwde komponent = new RekordLiniaSwde();
            _linie.Add(komponent);
            return komponent;
        }
    }
}
