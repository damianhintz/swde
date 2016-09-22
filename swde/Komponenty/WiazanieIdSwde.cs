using egib.swde.Sekcje;
using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Reprezentuje relację miezy rekordami w pliku SWDE.
    /// </summary>
    internal class WiazanieIdSwde : WiazanieSwdeBase
    {
        protected string _typ;

        /// <summary>
        /// Zwraca typ wskazywanego rekordu.
        /// </summary>
        public string Typ { get { return _typ; } }

        protected string _id;

        /// <summary>
        /// Zwraca identyfikator obiektu wskazywanego rekordu.
        /// </summary>
        public string Id { get { return _id; } }

        public WiazanieIdSwde(SekcjaObiektowSwde obiekty, string pole, string typ, string id) :
            base(obiekty, pole)
        {
            _typ = typ;
            _id = id;
        }

        public override bool GetRekord()
        {
            if (_rekord != null) return true;
            _rekord = _obiekty.SzukajObiektu(_typ, _id);
            return false;
        }

        public override string ToString()
        {
            return string.Format("Typ: {0}, Id: {1}", _typ, _id);
        }
    }
}
