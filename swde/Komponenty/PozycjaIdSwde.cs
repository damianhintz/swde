using egib.swde.Sekcje;
using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Wskazanie na rekord punktu.
    /// </summary>
    internal class PozycjaIdSwde : PozycjaSwde
    {
        protected string _typ;

        /// <summary>
        /// Typ wskazywanego rekordu.
        /// </summary>
        public string Typ { get { return _typ; } }

        protected string _id;

        /// <summary>
        /// Identyfikator obiektu wskazywanego rekordu.
        /// </summary>
        public string Id { get { return _id; } }

        /// <summary>
        /// Inicjalizuje instancję wskazania na rekord punktu.
        /// </summary>
        /// <param name="obiekty">Sekcja obiektów zawierająca wskazywany rekord.</param>
        /// <param name="typ"></param>
        /// <param name="id"></param>
        public PozycjaIdSwde(SekcjaObiektowSwde obiekty, string typ, string id)
            : base(obiekty, null, null)
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
