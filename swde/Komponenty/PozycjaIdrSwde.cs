using egib.swde.Sekcje;
using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Wskazanie na rekord punktu.
    /// </summary>
    internal class PozycjaIdrSwde : PozycjaSwde
    {
        protected string _idr;

        /// <summary>
        /// Zwraca identyfikator wskazywanego rekordu.
        /// </summary>
        public string Idr { get { return _idr; } }

        /// <summary>
        /// Inicjalizuje instancję wskazania na rekord punktu.
        /// </summary>
        /// <param name="obiekty">Sekcja obiektów, która zawiera wskazywany rekord.</param>
        /// <param name="idr">Identyfikator wskazywanego rekordu.</param>
        public PozycjaIdrSwde(SekcjaObiektowSwde obiekty, string idr)
            : base(obiekty, null, null)
        {
            _idr = idr;
        }

        public override bool GetRekord()
        {
            if (_rekord != null) return true;
            _rekord = _obiekty.SzukajRekordu(_idr);
            return false;
        }

        public override string ToString()
        {
            return string.Format("Idr: {0}", _idr);
        }
    }
}
