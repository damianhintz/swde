using System.Collections.Generic;

using swde.Sekcje;
using swde.Rekordy;
using swde.Komponenty;

namespace swde.Konstruktor
{
    /// <summary>
    /// Konstruktor/budowniczy pliku SWDE.
    /// </summary>
    /// <remarks>
    /// Odpowiedzialność: konstrukcja modelu pliku swde, zgodnie ze schematem tego pliku.
    /// Posiada szczegółową wiedzę na temat formatu pliku swde, więc kontroluje proces budowy jego składników.
    /// TODO: Klient może zarejestrować się i otrzymywać komunikaty o poszczególnych etapach budowy oraz ingerować w cały proces.
    /// </remarks>
    internal class BudowniczySwde
    {
        /// <summary>
        /// Stos komponentów, aby śledzić kontekst aktualnego komponentu.
        /// </summary>
        private Stack<KomponentBase> _aktualneKomponenty;

        protected DokumentBase _dokument;

        /// <summary>
        /// Czy konstrukcja dokumentu została zakończona.
        /// </summary>
        public bool CzyZbudowany
        {
            get
            {
                return _aktualneKomponenty.Count == 0 && _dokument != null && _dokument.Obiekty != null;
            }
        }

        private FabrykaKomponentow _fabryka;

        public BudowniczySwde(DokumentBase dokument)
        {
            _dokument = dokument;
            _fabryka = new FabrykaKomponentow(this);
            _aktualneKomponenty = new Stack<KomponentBase>();
        }

        /// <summary>
        /// Konwertuj linie pliku na obiektowy komponent modelu SWDE.
        /// </summary>
        /// <param name="linia"></param>
        public void WczytajLinia(string linia)
        {
            _fabryka.NowyKomponent(linia);
        }

        /// <summary>
        /// Zwróć bieżący komponent swde, ale nie zdejmuj go ze stosu.
        /// </summary>
        /// <returns>Zwraca aktualny komponent.</returns>
        private KomponentBase GetTop()
        {
            KontrolerKontekstu.Zapewnij(_aktualneKomponenty.Count > 0, "Brak nadrzędnego komponentu.");
            return _aktualneKomponenty.Peek();
        }

        /// <summary>
        /// Zakończ budowę bieżącego komponentu znajdującego się na stosie oraz zmień kontekst.
        /// Zmienia kontekst bieżącego komponentu, poprzez zdjęcie aktualnego komponentu ze stosu.
        /// </summary>
        /// <remarks>
        /// Trzeba podać poprawny/zgodny terminator komponentu.
        /// </remarks>
        /// <param name="terminator"></param>
        /// <returns>Zwraca stary kontekst/komponent.</returns>
        /// <exception cref="System.InvalidOperationException">Jeżeli nieprawidłowy terminator.</exception>
        private KomponentBase EndTop(KomponentBase terminator)
        {
            KontrolerKontekstu.Zapewnij(_aktualneKomponenty.Count > 0, "Nieoczekiwany terminator dokumentu.");
            KomponentBase top = _aktualneKomponenty.Pop(); //[top != null]
            top.End(terminator);
            return top;
        }

        public void DodajKomentarz(KomentarzSwde komponent)
        {
        }

        public void DodajMetadane(string nazwa, string wartosc, bool user = false)
        {
            var komponent = new MetadaneSwde(nazwa, wartosc, user);
            GetTop().DodajMetadane(komponent);
        }

        public void DodajAtrybut(string pole, string napis)
        {
            var komponent = new AtrybutSwde(pole, napis);
            GetTop().DodajAtrybut(komponent);
        }
        
        public void DodajWiazanie(string pole, string typ, string id)
        {
            var komponent = new WiazanieIdSwde(_dokument.Obiekty, pole, typ, id);
            GetTop().DodajWiazanie(komponent);
        }

        public void DodajWiazanie(string pole, string idr)
        {
            var komponent = new WiazanieIdrSwde(_dokument.Obiekty, pole, idr);
            GetTop().DodajWiazanie(komponent);
        }

        public void DodajPozycja(string x, string y, string z)
        {
            var komponent = new PozycjaSwde(_dokument.Obiekty, x, y, z);
            GetTop().DodajPozycja(komponent);
        }

        public void DodajPozycja(string typ, string id)
        {
            var komponent = new PozycjaIdSwde(_dokument.Obiekty, typ, id);
            GetTop().DodajPozycja(komponent);
        }

        public void DodajPozycja(string idr)
        {
            var komponent = new PozycjaIdrSwde(_dokument.Obiekty, idr);
            GetTop().DodajPozycja(komponent);
        }
        
        public void DodajWezelKoncowy()
        {
            GetTop().DodajWezelKoncowy();
        }

        public void DodajKonturZewnetrzny()
        {
            GetTop().DodajKonturZew();
        }

        public void DodajKonturWewnetrzny()
        {
            GetTop().DodajKonturWew();
        }

        /// <summary>
        /// Linia lub obszar, zależnie od kontekstu.
        /// Typ linii będzie określany przez aktualny komponent na stosie.
        /// </summary>
        public void ZacznijBudowacLinieLubObszar()
        {
            var komponent = GetTop().DodajLiniaLubObszar();
            _aktualneKomponenty.Push(komponent);
        }

        public void ZakonczBudoweLiniiLubObszaru()
        {
            EndTop(new TerminatorLiniiLubObszaruSwde());
        }

        public void ZacznijBudowacDefinicjeTypu(DefinicjaTypuSwde komponent)
        {
            GetTop().DodajDefinicjaTypu(komponent);
            _aktualneKomponenty.Push(komponent);
        }
        
        public void ZacznijBudowacRekordOpisowy(string kod, string typ, string id, string idr, string st_obj)
        {
            var komponent = new RekordOpisowySwde(kod, typ, id, idr, st_obj);
            GetTop().DodajRekordOpisowy(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacRekordZlozony(string kod, string typ, string id, string idr, string st_obj)
        {
            var komponent = new RekordZlozonySwde(kod, typ, id, idr, st_obj);
            GetTop().DodajRekordZlozony(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacRekordPunktowy(string kod, string typ, string id, string idr, string st_obj)
        {
            var komponent = new RekordPunktowySwde(kod, typ, id, idr, st_obj);
            GetTop().DodajRekordPunktowy(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacRekordLiniowy(string kod, string typ, string id, string idr, string st_obj)
        {
            var komponent = new RekordLiniowySwde(kod, typ, id, idr, st_obj);
            GetTop().DodajRekordLiniowy(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacRekordObszarowy(string kod, string typ, string id, string idr, string st_obj)
        {
            var komponent = new RekordObszarowySwde(kod, typ, id, idr, st_obj);
            GetTop().DodajRekordObszarowy(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZakonczBudoweRekorduLubTypu()
        {
            EndTop(new TerminatorRekorduLubTypuSwde());
        }

        public void ZacznijBudowacSekcjeMetadanych()
        {
            var komponent = new SekcjaMetadanychSwde();
            GetTop().DodajSekcjaMetadanych(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacSekcjeAtrybutow()
        {
            var komponent = new SekcjaAtrybutowSwde();
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacSekcjeTypow()
        {
            var komponent = new SekcjaTypowSwde();
            _aktualneKomponenty.Push(komponent);
        }

        public void ZacznijBudowacSekcjeObiektow()
        {
            var komponent = new SekcjaObiektowSwde();
            GetTop().DodajSekcjaObiektow(komponent);
            _aktualneKomponenty.Push(komponent);
        }

        public void ZakonczBudoweSekcji()
        {
            EndTop(new TerminatorSekcjiSwde());
        }

        public void ZacznijBudowacSwde()
        {
            KontrolerKontekstu.Zapewnij(_aktualneKomponenty.Count == 0, "Nagłówek dokumentu można zdefiniować tylko raz.");
            _aktualneKomponenty.Push(_dokument);
        }

        public void ZakonczBudoweSwde()
        {
            //_dokument = EndTop(new TerminatorSwde()) as DokumentBase;
            EndTop(new TerminatorSwde());
        }
    }
}
