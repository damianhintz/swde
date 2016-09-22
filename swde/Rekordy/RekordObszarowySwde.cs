using System.Collections.Generic;

using swde.Komponenty;

namespace swde.Rekordy
{
    /// <summary>
    /// Opis przestrzenny rekordu jest zbiorem obszarów z enklawami.
    /// </summary>
    internal class RekordObszarowySwde : RekordSwdeBase
    {
        public override string TypBazowy
        {
            get
            {
                return "RO";
            }
        }

        private List<RekordObszarSwde> _obszary;

        /// <summary>
        /// Zwraca kolekcję obszarów.
        /// </summary>
        public IEnumerable<RekordObszarSwde> Obszary { get { return _obszary; } }

        public RekordObszarowySwde(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
            _obszary = new List<RekordObszarSwde>();
        }

        /// <summary>
        /// Dodaj nowy obszar.
        /// </summary>
        /// <returns></returns>
        public override RekordLiniaSwde DodajLiniaLubObszar()
        {
            RekordObszarSwde komponent = new RekordObszarSwde();
            _obszary.Add(komponent);
            return komponent;
        }
    }
}
