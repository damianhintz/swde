using System.Collections.Generic;
using System.Text.RegularExpressions;

using swde.Komponenty;

namespace swde.Rekordy
{
    /// <summary>
    /// Rekord reprezentuje stan obiektu.
    /// </summary>
    /// <remarks>
    /// Do jednego obiektu może odnosić się wiele rekordów, reprezentujących różne stany obiektu (wersje obiektu).
    /// Rekordy odnoszące się do jednego obiektu mają w pliku SWDE ten sam identyfikator obiektu.
    /// Rekord nie posiadający żadnego z identyfikatorów nie może być przedmiotem wskazania z innego rekordu.
    /// </remarks>
    internal abstract class RekordSwdeBase : KomponentBase
    {
        private RekordId _rekordId;

        public RekordId Identyfikator { get { return _rekordId; } }

        protected string _kod;

        /// <summary>
        /// Kod obiektu określa przynależność do klasy.
        /// Obiekty klasy mają wspólne cechy oraz relacje.
        /// </summary>
        /// <remarks>
        /// Kod może być pusty.
        /// Gdy klasa obiektu nie jest określona to uznajemy, że 
        /// rekord reprezentuje obiekt należący do klasy zgodnej z typem aplikacyjnym rekordu. 
        /// </remarks>
        public string Kod
        {
            get
            {
                return string.IsNullOrEmpty(_kod) ? Typ : _kod;
            }
        }

        /// <summary>
        /// Typ bazowy rekordu (RD | RC | RP | RL | RO).
        /// </summary>
        public abstract string TypBazowy { get; }

        protected string _typ;

        /// <summary>
        /// Typ aplikacyjny rekordu określa sposób prezentacji danych o obiekcie w pliku SWDE.
        /// </summary>
        /// <remarks>
        /// Typ rekordu może być pusty.
        /// Typ bazowy, określa składnię rekordu użytego do opisu obiektów.
        /// Gdy pole typ rekordu jest puste to uznajemy, że typem rekordu jest jego typ bazowy.
        /// </remarks>
        public string Typ
        {
            get
            {
                return string.IsNullOrEmpty(_typ) ? TypBazowy : _typ;
            }
        }

        protected string _id;

        /// <summary>
        /// Identyfikator obiektu (identyfikator obiektu w systemie bazie danych dostawcy).
        /// Rekordy odnoszące się do jednego obiektu mają ten sam identyfikator obiektu.
        /// </summary>
        /// <remarks>
        /// Jest on określony w ramach typu.
        /// W formacie 3.00 to jest idIIP.
        /// </remarks>
        public string Id { get { return _id; } }

        protected string _idr;

        /// <summary>
        /// Identyfikator rekordu, unikalny w ramach pliku.
        /// W formacie SWDE każdy rekord posiada dwa identyfikatory.
        /// Identyfikator rekordu jest niepowtarzalną nazwą rekordu w ramach pliku SWDE.
        /// </summary>
        /// <remarks>
        /// Identyfikator rekordu może być pusty.
        /// W formacie 3.00 to jest versionID tzw. identyfikator wersji.
        /// Identyfikator wersji ma postac daty zgodnie z ISO 8601.
        /// Wartosc identyfikatora wersji dla wczesniejszej wersji musi byc mniejsza 
        /// (w sensie porównania ciagu znaków) niz wartosc dla wersji pózniejszej.
        /// </remarks>
        public string Idr { get { return _idr; } }

        protected string _st_obj;

        /// <summary>
        /// Stan obiektu.
        /// Może być tylko jeden rekord odnoszący się do aktualnego obiektu.
        /// Rekordy z pustym identyfikatorem obiektu odnoszą się do różnych obiektów.
        /// </summary>
        /// <remarks>
        /// Dwie cyfry według wzoru:
        /// Przynależność: 0 | 1 | 2
        /// 0 - nieokreślona – domyślnie baza danych
        /// 1 - obiekt bazy danych
        /// 2 - obiekt pomocniczy – może być pominięty przy imporcie
        /// Wersja: 0 | 1 | 2
        /// 0 - nieokreślona, domyślnie wersja aktualna
        /// 1 - aktualna
        /// 2 - poprzednia lub obiekt usunięty
        /// </remarks>
        public string StanObiektu { get { return _st_obj; } }

        /// <summary>
        /// Przynależność obiektu.
        /// </summary>
        public PrzynaleznoscObiektu Przynaleznosc
        {
            get
            {
                PrzynaleznoscObiektu przynaleznosc = PrzynaleznoscObiektu.BazaDanych;

                switch (_st_obj[0])
                {
                    case '0':
                    case '1':
                        przynaleznosc = PrzynaleznoscObiektu.BazaDanych;
                        break;
                    case '2':
                        przynaleznosc = PrzynaleznoscObiektu.Pomocniczy;
                        break;
                    default:
                        KontrolerKontekstu.Zapewnij(false,
                            string.Format("Przynależność obiektu nieprawidłowa <{0}>.", _st_obj[0]));
                        break;
                }

                return przynaleznosc;
            }
        }

        /// <summary>
        /// Wersja obiektu.
        /// </summary>
        public WersjaObiektu Wersja
        {
            get
            {
                WersjaObiektu wersja = WersjaObiektu.Aktualna;

                switch (_st_obj[1])
                {
                    case '0':
                    case '1':
                        wersja = WersjaObiektu.Aktualna;
                        break;
                    case '2':
                        wersja = WersjaObiektu.Archiwalna;
                        break;
                    default:
                        KontrolerKontekstu.Zapewnij(false, 
                            string.Format("Wersja obiektu nieprawidłowa <{0}>.", _st_obj[1]));
                        break;
                }

                return wersja;
            }
        }
        
        private List<AtrybutSwde> _atrybuty;

        /// <summary>
        /// Zwraca kolekcję atrybutów obiektu.
        /// </summary>
        public IEnumerable<AtrybutSwde> Atrybuty { get { return _atrybuty; } }

        private List<WiazanieSwdeBase> _wiazania;

        /// <summary>
        /// Zwraca kolekcję wiązań obiektu.
        /// </summary>
        public IEnumerable<WiazanieSwdeBase> Wiazania { get { return _wiazania; } }

        public RekordSwdeBase(string kod, string typ, string id, string idr, string st_obj)
        {
            _rekordId = new RekordId(id, idr);

            _kod = kod;
            _typ = typ;
            _id = id;
            _idr = idr;
            _st_obj = st_obj;

            _atrybuty = new List<AtrybutSwde>();
            _wiazania = new List<WiazanieSwdeBase>();
        }
        
        private void ZapewnijNotNull(KomponentBase komponent)
        {
            KontrolerKontekstu.Zapewnij(komponent != null, "Nie można dodać pustego komponentu do rekordu.");
        }

        private void ZapewnijFormatStObj()
        {
            Regex regex = new Regex("[0-2][0-2]");
            //przynależność, wersja
            KontrolerKontekstu.Zapewnij(regex.IsMatch(_st_obj), "");
        }

        /// <summary>
        /// Dodaj atrybut rekordu.
        /// </summary>
        /// <param name="komponent"></param>
        public override void DodajAtrybut(AtrybutSwde komponent)
        {
            ZapewnijNotNull(komponent);
            _atrybuty.Add(komponent);
        }

        /// <summary>
        /// Dodaj wiązanie rekordu.
        /// </summary>
        /// <param name="komponent"></param>
        public override void DodajWiazanie(WiazanieSwdeBase komponent)
        {
            ZapewnijNotNull(komponent);
            _wiazania.Add(komponent);
        }
    }
}
