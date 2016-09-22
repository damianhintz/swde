using egib.swde.Sekcje;
using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Reprezentuję relację między rekordami w pliku SWDE.
    /// </summary>
    internal class WiazanieIdrSwde : WiazanieSwdeBase
    {
        protected string _idr;

        /// <summary>
        /// Zwraca identyfikator rekordu będącego w relacji.
        /// </summary>
        public string Idr { get { return _idr; } }

        public WiazanieIdrSwde(SekcjaObiektowSwde obiekty, string pole, string idr) :
            base(obiekty, pole)
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
