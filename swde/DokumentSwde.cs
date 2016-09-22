using System.Collections.Generic;
using egib.swde.Sekcje;
using egib.swde.Rekordy;
using egib.swde.Komponenty;

namespace egib.swde
{
    /// <summary>
    /// Plik SWDE podzielony jest na cztery sekcje. Pierwsze trzy sekcje to metadane.
    /// Pierwsza linia pliku pozwala na identyfikację, że dany plik zapisany jest w formacie SWDE.
    /// Dane dotyczące obiektów zapisane są w sekcji obiektów.
    /// </summary>
    public class DokumentSwde
    {
        private DokumentBase _dokument;
        private string _nazwa;
        private Dictionary<RekordId, ObiektSwde> _obiekty;
        
        public DokumentSwde(string fileName, bool ukladGeodezyjny = true)
        {
            Geodezyjny = ukladGeodezyjny;
            _nazwa = fileName;
            _dokument = new DokumentReader(fileName);
            _obiekty = new Dictionary<RekordId, ObiektSwde>();
        }

        public bool Geodezyjny { get; private set; }

        public int Count { get { return _dokument.Obiekty.Count; } }

        public string Nazwa { get { return _nazwa; } }

        public int GetCount(string typ)
        {
            typ = RekordSwdeG5.NormalizujPrefiksTypu(typ);
            TabelaSwde tabela = _dokument.Obiekty.SzukajTabeli(typ);
            if (tabela == null) return 0;
            return tabela.Count;
        }

        public IEnumerable<ObiektSwde> GetObiekty()
        {
            return GetRekordy(_dokument.Obiekty);
        }

        public IEnumerable<ObiektSwde> GetObiektyKlasy(string typ)
        {
            typ = RekordSwdeG5.NormalizujPrefiksTypu(typ);
            TabelaSwde tabela = _dokument.Obiekty.SzukajTabeli(typ);
            if (tabela == null) return new List<ObiektSwde>();
            return GetRekordy(tabela.Obiekty);
        }

        /// <summary>
        /// Ogólna funkcja do wyszukiwania obiektów. Rekordy swde opakowywane są w klasę ObiektSwde.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ObiektSwde> GetRekordy(IEnumerable<RekordSwdeBase> rekordy)
        {
            List<ObiektSwde> obiekty = new List<ObiektSwde>();

            foreach (RekordSwdeBase rekord in rekordy)
            {
                //if (rekord.Przynaleznosc == PrzynaleznoscObiektu.Pomocniczy) continue;
                if (rekord.Wersja == WersjaObiektu.Archiwalna) continue;
                obiekty.Add(createObiekt(rekord));
            }

            return obiekty;
        }

        /// <summary>
        /// Zwraca obiekt odpowiadający danemu rekordowi, jeżeli taki obiekt jeszcze nie istnieje to zostanie utworzony.
        /// </summary>
        /// <param name="rekord"></param>
        /// <returns></returns>
        internal ObiektSwde createObiekt(RekordSwdeBase rekord)
        {
            if (_obiekty.ContainsKey(rekord.Identyfikator)) return _obiekty[rekord.Identyfikator];
            ObiektSwde obiekt = new ObiektSwde(this, rekord);
            _obiekty.Add(rekord.Identyfikator, obiekt);
            return obiekt;
        }

        /// <summary>
        /// Wyszukuje poprzednią wersję obiektu.
        /// </summary>
        /// <returns>Poprzednia wersja obiektu lub <code>null</code> jeżeli brak poprzedniej wersji.</returns>
        internal IEnumerable<ObiektSwde> GetWersjeObiektu(RekordSwdeBase rekord)
        {
            SekcjaObiektowSwde obiekty = _dokument.Obiekty;
            KolekcjaWersji wersjeRekordu = obiekty.SzukajWersji(rekord.Typ, rekord.Id);
            List<ObiektSwde> wersjeObiektu = new List<ObiektSwde>();

            //UWAGA: dostęp do daty wymaga znajomości modelu G5 (to jest wyższy poziom abstrakcji).
            foreach (RekordSwdeBase wersjaRekordu in wersjeRekordu)
            {
                wersjeObiektu.Add(createObiekt(wersjaRekordu));
            }

            return wersjeObiektu;
        }
    }
}
