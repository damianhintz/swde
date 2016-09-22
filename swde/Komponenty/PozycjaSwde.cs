using egib.swde.Sekcje;
using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Pozycja punktu.
    /// </summary>
    /// <remarks>
    /// Określenie geodezyjnego układu odniesienia, w którym zostały podane współrzędne znajduje się w sekcji nagłówkowej.
    /// Układ współrzędnych formatu SWDE to układ kartezjański o kolejnych współrzędnych.
    /// Wszystkie wartości współrzędnych wyrażone są w metrach.
    /// </remarks>
    internal class PozycjaSwde : ReferencjaSwde
    {
        protected string _x; //N - na północ.

        /// <summary>
        /// Zwraca współrzędną X punktu.
        /// </summary>
        public string X { get { return GetX(); } }

        protected string _y; //E - na wschód.

        /// <summary>
        /// Zwraca współrzędną Y punktu.
        /// </summary>
        public string Y { get { return GetY(); } }

        protected string _z; //H - do góry, może być pusty

        /// <summary>
        /// Zwraca współrzędną Z punktu.
        /// </summary>
        public string Z { get { return GetZ(); } }

        public PozycjaSwde(SekcjaObiektowSwde obiekty, string x, string y, string z = "")
            : base(obiekty)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public override bool GetRekord()
        {
            return true;
        }

        protected string GetX()
        {
            if (GetRekord()) return _x;

            RekordPunktowySwde rekordPunktu = Rekord as RekordPunktowySwde;
            PozycjaSwde pozycjaPunktu = rekordPunktu.Pozycja;

            //Do tego miejsca dochodzimy tylko raz, więc trzeba zapamiętać wszystkie rzędne.
            _y = pozycjaPunktu.Y;
            _z = pozycjaPunktu.Z;

            return _x = pozycjaPunktu.X;
        }

        protected string GetY()
        {   
            if (GetRekord()) return _y;

            RekordPunktowySwde rekordPunktu = Rekord as RekordPunktowySwde;
            PozycjaSwde pozycjaPunktu = rekordPunktu.Pozycja;

            _x = pozycjaPunktu.X;
            _z = pozycjaPunktu.Z;

            return _y = pozycjaPunktu.Y;
        }

        protected string GetZ()
        {
            if (GetRekord()) return _z;

            RekordPunktowySwde rekordPunktu = Rekord as RekordPunktowySwde;
            PozycjaSwde pozycjaPunktu = rekordPunktu.Pozycja;

            _x = pozycjaPunktu.X;
            _y = pozycjaPunktu.Y;

            return _z = pozycjaPunktu.Z;
        }
    }
}
