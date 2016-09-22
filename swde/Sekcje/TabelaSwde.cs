using System.Collections;
using System.Collections.Generic;

using egib.swde.Rekordy;

namespace egib.swde.Sekcje
{
    /// <summary>
    /// Kolekcja obiektów tego samego typu.
    /// </summary>
    internal class TabelaSwde
    {
        private string _typ;

        public string Typ { get { return _typ; } }

        private Dictionary<string, KolekcjaWersji> _wersje;

        private List<RekordSwdeBase> _obiekty;

        public IEnumerable<RekordSwdeBase> Obiekty { get { return _obiekty; } }

        public int Count { get { return _obiekty.Count; } }

        public TabelaSwde(string typ)
        {
            _typ = typ;
            _wersje = new Dictionary<string, KolekcjaWersji>();
            _obiekty = new List<RekordSwdeBase>();
        }

        /// <summary>
        /// Dodaj obiekt do tabeli (może być nowa wersja).
        /// </summary>
        /// <param name="rekord"></param>
        public bool DodajObiekt(RekordSwdeBase rekord)
        {
            string id = rekord.Id;

            if (!_wersje.ContainsKey(id))
            {
                _wersje.Add(id, new KolekcjaWersji(id));
            }

            if (!_wersje[id].DodajWersje(rekord))
            {
                return false;
            }

            _obiekty.Add(rekord);

            return true;
        }

        /// <summary>
        /// Szukaj wszystkich wersji danego obiektu.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null, jeżeli brak.</returns>
        public KolekcjaWersji SzukajWersji(string id)
        {
            if (!_wersje.ContainsKey(id)) return null;
            return _wersje[id];
        }

        /// <summary>
        /// Szukaj aktualnej wersji danego obiektu.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null, jeżeli brak.</returns>
        public RekordSwdeBase SzukajObiektu(string id)
        {
            if (!_wersje.ContainsKey(id)) return null;
            return _wersje[id].Aktualna;
        }
    }
}
