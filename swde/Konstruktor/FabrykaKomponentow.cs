using System;

using egib.swde.Rekordy;
using egib.swde.Komponenty;

namespace egib.swde.Konstruktor
{
    /// <summary>
    /// Fabryka/konstruktor komponentów z linii pliku SWDE.
    /// </summary>
    /// <remarks>
    /// Linia formatu SWDE jest jedną linią tekstu. Podzielona jest na pola rozdzielone przecinkami.
    /// Jeżeli ostanie pole nie jest polem danych lub etykiety to linia SWDE kończy się średnikiem.
    /// W liniach za średnikiem można umieszczać dowolny tekst, który traktowany jest jako komentarz.
    /// Pierwszym polem linii jest jej wyróżnik całkowicie określający składnię linii.
    /// 
    /// Pole jest najmniejszą leksykalną jednostką w formacie SWDE.
    /// Pola w linii zakończone są przecinkiem, poza ostatnim polem, które zakończone jest średnikiem lub znakiem końca linii. 
    /// Interpretacja zawartości pola zależy od “lewego kontekstu” (ciągu pól je poprzedzających w linii). 
    /// Lewy kontekst pierwszego pola linii (wyróżnika) jest pusty.
    /// </remarks>
    internal class FabrykaKomponentow
    {
        private static readonly char[] _separatorPola = ",;".ToCharArray();

        private static string[] _polaLinii;
        private static string _linia;

        private BudowniczySwde _budowniczy;

        /// <summary>
        /// Inicjalizuje fabrykę komponentów. Przekazuje konstruowane komponenty budowniczemu.
        /// </summary>
        /// <param name="budowniczy"></param>
        public FabrykaKomponentow(BudowniczySwde budowniczy)
        {
            _budowniczy = budowniczy;
        }

        private void ZapewnijSymbolNotNull(string liniaString)
        {
            throw new ParseSwdeException(
                string.Format("Niepoprawny format wiersza w linii <{0}>.", liniaString),
                -1, liniaString);
        }

        private void ZapewnijMinimalnaLiczbaPol(int minimalnaLiczba)
        {
            if (_polaLinii.Length < minimalnaLiczba)
                throw new ArgumentException("Niepoprawny format wiersza, brak wymaganej liczby pól.");
        }

        /// <summary>
        /// Utwórz nowy komponent z linii tekstu w pliku SWDE.
        /// </summary>
        /// <param name="liniaTekstu"></param>
        /// <returns></returns>
        /// <exception cref="ParseSwdeException">Jeżeli nierozpoznana linia.</exception>
        /// <exception cref="ArgumentException">Jeżeli niepoprawny format linii.</exception>
        public void NowyKomponent(string liniaTekstu)
        {
            SplitPola(liniaTekstu);
            FabrykujKomponent();
        }

        /// <summary>
        /// Rozdziela linie pliku na pola.
        /// </summary>
        /// <param name="linia"></param>
        private void SplitPola(string linia)
        {
            _linia = linia;
            _polaLinii = linia.Split(_separatorPola);
            NormalizujPola();
        }

        /// <summary>
        /// Usuwa skrajne spacje z każdego pola.
        /// </summary>
        private void NormalizujPola()
        {
            for (int i = 0; i < _polaLinii.Length; i++)
            {
                _polaLinii[i] = _polaLinii[i].Trim();
            }
        }

        private void FabrykujKomponent()
        {
            switch (_polaLinii[0])
            {
                case "SWDE.w.2.00.(C) GUGiK 2000": //wersja 2.00, nagłówek pliku
                    _budowniczy.ZacznijBudowacSwde();
                    break;
                //case "SWDE.w.3.00.(C) GUGiK 2011": //wersja 3.00, nagłówek pliku
                case "SWDEX":
                case "SWDEXC":
                    _budowniczy.ZakonczBudoweSwde();
                    break;
                case "SN": //sekcja metadanych.
                    _budowniczy.ZacznijBudowacSekcjeMetadanych();
                    break;
                case "NS": //rekord nagłówkowy systemu.
                    ZapewnijMinimalnaLiczbaPol(2);
                    _budowniczy.DodajMetadane(_polaLinii[1], _polaLinii[2]);
                    break;
                case "NU": //rekord nagłówkowy użytkownika.
                    ZapewnijMinimalnaLiczbaPol(2);
                    _budowniczy.DodajMetadane(_polaLinii[1], _polaLinii[2], true);
                    break; //traktujemy jak komentarz
                case "SP": //sekcja atrybutów
                    _budowniczy.ZacznijBudowacSekcjeAtrybutow();
                    break;
                case "W": //deklaracja wiązania
                case "B": //deklaracja atrybutu
                    break; ; //traktujemy jak komentarz
                case "ST": //sekcja typów
                    _budowniczy.ZacznijBudowacSekcjeTypow();
                    break;
                case "TD": //definicja typu
                    _budowniczy.ZacznijBudowacDefinicjeTypu(new DefinicjaTypuSwde());
                    break;
                case "TP": //deklaracja pola atrybutu
                case "TPW":
                case "TPN":
                    break; //traktujemy jak komentarz
                case "WE": //deklaracja elementu
                case "WP":
                    break; //traktujemy jak komentarz
                case "WR": //deklaracja pola wiązania
                case "WW":
                case "WN":
                    break; //traktujemy jak komentarz
                case "SO": //sekcja obiektów/rekordów
                    _budowniczy.ZacznijBudowacSekcjeObiektow();
                    break;
                case "SX":
                case "SXC":
                    _budowniczy.ZakonczBudoweSekcji();
                    break;
                case "RD": //rekord opisowy: RD, [KOD], [TYP], [ID], [IDR], [ST_OBJ];
                    ZapewnijMinimalnaLiczbaPol(5);
                    _budowniczy.ZacznijBudowacRekordOpisowy(_polaLinii[1], _polaLinii[2], _polaLinii[3], _polaLinii[4], _polaLinii[5]);
                    break;
                case "RC": //rekord złozony: RC, [KOD], [TYP], [ID], [IDR], [ST_OBJ];
                    ZapewnijMinimalnaLiczbaPol(5);
                    _budowniczy.ZacznijBudowacRekordZlozony(_polaLinii[1], _polaLinii[2], _polaLinii[3], _polaLinii[4], _polaLinii[5]);
                    break;
                case "RP": //rekord punktowy: RP, [KOD], [TYP], [ID], [IDR], [ST_OBJ];
                    ZapewnijMinimalnaLiczbaPol(5);
                    _budowniczy.ZacznijBudowacRekordPunktowy(_polaLinii[1], _polaLinii[2], _polaLinii[3], _polaLinii[4], _polaLinii[5]);
                    break;
                case "RL": //rekord liniowy: RL, [KOD], [TYP], [ID], [IDR], [ST_OBJ];
                    ZapewnijMinimalnaLiczbaPol(5);
                    _budowniczy.ZacznijBudowacRekordLiniowy(_polaLinii[1], _polaLinii[2], _polaLinii[3], _polaLinii[4], _polaLinii[5]);
                    break;
                case "RO": //rekord obszarowy: RO, [KOD], [TYP], [ID], [IDR], [ST_OBJ];
                    ZapewnijMinimalnaLiczbaPol(5);
                    _budowniczy.ZacznijBudowacRekordObszarowy(_polaLinii[1], _polaLinii[2], _polaLinii[3], _polaLinii[4], _polaLinii[5]);
                    break;
                //case "RM": //rekord modelu terenu:
                case "GL": //linia, obszar
                    _budowniczy.ZacznijBudowacLinieLubObszar();
                    break;
                case "IL": //Id. linii: IL, KOD_ELEM, [NAZ_ELEM];
                    break;
                case "PZ": //linia domknięcia: PZ;
                    _budowniczy.DodajWezelKoncowy();
                    break;
                case "IP": //Id. wierzchołka: IP, KOD_ELEM, [NAZ_ELEM]
                    break;
                //opis połączenia OL; | OAD; | OAM; - brak jest równoważny odcinkowi prostej
                case "OL": //odcinek prostej: OL;
                    break;
                case "OAD": //duży łuk, promień dodatni w prawo: OAD, R;
                case "OAM": //mały łuk, promień dodatni w prawo: OAM, R;
                    throw new ApplicationException("Konwersja łuków nie jest obsługiwana.");
                case "K": //kontur:
                    ZapewnijMinimalnaLiczbaPol(1);
                    switch (_polaLinii[1])
                    {
                        case "+": //K, +; kontur zewnętrzny
                            _budowniczy.DodajKonturZewnetrzny();
                            break;
                        case "-": //K, -; kontur wewnętrzny
                            _budowniczy.DodajKonturWewnetrzny();
                            break;
                        //default:
                        //return null;
                    }
                    break;
                case "X":
                case "XC":
                    _budowniczy.ZakonczBudoweRekorduLubTypu();
                    break;
                case "GX":
                case "GXC":
                    _budowniczy.ZakonczBudoweLiniiLubObszaru();
                    break;
                case "D":  //atrybuty: D, POLE, D, napis
                    ZapewnijMinimalnaLiczbaPol(3);
                    string[] polaLinii = _linia.Split(new char[] { ',' }, 4);
                    _budowniczy.DodajAtrybut(_polaLinii[1], polaLinii[3]);
                    break;
                case "WG": //wiązania: WG, POLE, TYP, ID;
                    ZapewnijMinimalnaLiczbaPol(3);
                    _budowniczy.DodajWiazanie(_polaLinii[1], _polaLinii[2], _polaLinii[3]);
                    break;
                case "WL": //wiązania: WL, POLE, IDR;
                    ZapewnijMinimalnaLiczbaPol(2);
                    _budowniczy.DodajWiazanie(_polaLinii[1], _polaLinii[2]);
                    break;
                case "P": //pozycja:
                    ZapewnijMinimalnaLiczbaPol(4);
                    switch (_polaLinii[1])
                    {
                        case "G": //P, G, X, Y, [Z];
                            _budowniczy.DodajPozycja(_polaLinii[2], _polaLinii[3], _polaLinii[4]);
                            break;
                        case "P": //P, P, TYP, ID;
                            _budowniczy.DodajPozycja(_polaLinii[2], _polaLinii[3]);
                            break;
                        case "K": //P, K, IDR;
                            _budowniczy.DodajPozycja(_polaLinii[2]);
                            break;
                        //default: return null;
                    }
                    break;
                case "":
                case "C": //komentarz
                    break;
                default: // wyjątek
                    ZapewnijSymbolNotNull(_polaLinii[0]);
                break;
            }
        }
    }
}
