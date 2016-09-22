using System;
using System.Collections.Generic;
using System.Linq;
using swde.Rekordy;
using swde.Komponenty;

namespace swde
{
    /// Klient nie może tworzyć bezpośrednio obiektów tej klasy.
    /// Klient uzyskuje dostęp do ich instacji tylko poprzez DokumentSwde.
    public class ObiektSwde
    {
        private DokumentSwde _dokument;
        private RekordSwdeBase _rekord;
        private ObiektId _id;
        private GeometriaSwde _geometria;

        internal ObiektSwde(DokumentSwde dokument, RekordSwdeBase rekord)
        {
            _dokument = dokument;
            _rekord = rekord;
            //_geometria = GeometriaSwde.createGeometryOrNull(dokument, rekord);
            _id = new ObiektId(_rekord.Identyfikator);
        }

        /// <summary>
        /// Unikalny identyfikator obiektu (w ramach danego pliku swde).
        /// Nie można polegać na identyfikatorze rekordu z pliku SWDE ponieważ może być pusty lub powtórzony.
        /// </summary>
        public ObiektId Id { get { return _id; } }

        public string Typ { get { return RekordSwdeG5.NormalizujPrefiksTypu(_rekord.Typ); } }

        public GeometriaSwde Geometria
        {
            get
            {
                if (_geometria == null)
                    _geometria = GeometriaSwde.createGeometryOrNull(_dokument, _rekord);
                return _geometria;
            }
        }

        /// <summary>
        /// Zwraca wartość danego atrybutu.
        /// Atrybut jest wymagany, czyli musi wystąpić.
        /// </summary>
        /// <param name="nazwa">Nazwa/kod atrybutu (bez prefiksu np. G5).</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Jeżeli brak atrybutu.</exception>
        public string GetAtrybut(string nazwa)
        {
            return GetAtrybutWielokrotny(nazwa).First();
        }

        /// <summary>
        /// Zwraca pierwszy atrybut o podanej nazwie lub wartość domyślną jeżeli nie ma takiego atrybutu.
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="domyslny"></param>
        /// <returns></returns>
        public string GetAtrybutLub(string nazwa, string domyslny = "")
        {
            IEnumerable<string> atrybuty = GetAtrybutWielokrotny(nazwa);

            foreach (string atrybut in atrybuty)
            {
                return atrybut;
            }

            return domyslny;
        }

        /// <summary>
        /// Zwraca wartość atrybutu wielokrotnego.
        /// </summary>
        /// <param name="nazwa">Nazwa/kod atrybut (bez prefiksu).</param>
        /// <returns></returns>
        public IEnumerable<string> GetAtrybutWielokrotny(string nazwa)
        {
            nazwa = RekordSwdeG5.NormalizujPrefiksAtrybutu(nazwa);

            IEnumerable<AtrybutSwde> atrybuty = _rekord.Atrybuty.Where(a => a.Pole == nazwa);

            List<string> napisy = new List<string>();

            foreach (var atrybut in atrybuty)
            {
                napisy.Add(atrybut.Napis);
            }

            return napisy;
        }

        public IEnumerable<Tuple<string, string>> GetAtrybuty()
        {
            List<Tuple<string, string>> atrybuty = new List<Tuple<string, string>>();
            foreach (var atrybut in _rekord.Atrybuty) atrybuty.Add(new Tuple<string, string>(atrybut.Pole, atrybut.Napis));
            return atrybuty;
        }

        /// <summary>
        /// Zwraca obiekt będący w danej relacji z tym obiektem. Musi być dokładnie jeden powiązany rekord.
        /// </summary>
        /// <param name="nazwa">Nazwa/kod relacji (bez prefiksu).</param>
        public ObiektSwde GetRelacja(string nazwa)
        {
            return GetRelacjaWielokrotna(nazwa).First();
        }

        /// <summary>
        /// Zwraca pierwszy obiekt w relacji o podanej nazwie lub null jeżeli brak takiej relacji.
        /// </summary>
        public ObiektSwde GetRelacjaLubNull(string nazwa)
        {
            IEnumerable<ObiektSwde> obiekty = GetRelacjaWielokrotna(nazwa);

            foreach (var obiekt in obiekty)
            {
                return obiekt;
            }

            return null;
        }

        /// <summary>
        /// Zwraca obiekty będące w danej relacji z tym obiektem.
        /// </summary>
        /// <param name="nazwa">Nazwa/kod relacji (bez prefiksu).</param>
        public IEnumerable<ObiektSwde> GetRelacjaWielokrotna(string nazwa)
        {
            nazwa = RekordSwdeG5.NormalizujPrefiksAtrybutu(nazwa);

            IEnumerable<WiazanieSwdeBase> wiazania = _rekord.Wiazania.Where(r => r.Pole == nazwa);

            List<ObiektSwde> obiekty = new List<ObiektSwde>();

            foreach (WiazanieSwdeBase wiazanie in wiazania)
            {
                RekordSwdeBase rekord = wiazanie.Rekord;
                //Obiekt odpowiadający rekordowi powinien już istnieć i dokument powinien posiadać instancję tego obiektu.
                obiekty.Add(_dokument.createObiekt(rekord));
            }

            return obiekty;
        }

        /// <summary>
        /// Zwraca wszystkie wersje danego obiektu.
        /// </summary>
        public IEnumerable<ObiektSwde> GetWersje(bool tezThis = true)
        {
            if (tezThis) return _dokument.GetWersjeObiektu(_rekord);

            return _dokument.GetWersjeObiektu(_rekord).Where(obiekt => obiekt._id != this._id);
        }

        /// <summary>
        /// Zwraca nowszą wersję obiektu.
        /// </summary>
        public ObiektSwde GetNastepnaWersja()
        {
            SortedList<string, ObiektSwde> obiekty = new SortedList<string, ObiektSwde>();

            //Posortować po DTW.
            foreach (var obiekt in GetWersje(true))
            {
                string dtw = obiekt.GetAtrybutLub("G5DTW");
                obiekty.Add(dtw, obiekt);
            }

            string dtwPoprzednie = GetAtrybutLub("G5DTW"); //Atrybut bieżącego obiektu.

            //Znaleźć pierwszy obiekt, którego dtw jest większe od tego obiektu.
            foreach (var kv in obiekty)
            {
                string dtwNastepne = kv.Key;

                if (dtwNastepne.CompareTo(dtwPoprzednie) > 0)
                {
                    return kv.Value;
                }
            }

            //Nie znaleziono nowszej wersji.
            return null;
        }
    }
}
