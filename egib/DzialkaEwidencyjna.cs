using System;
using System.Collections.Generic;
using Pragmatic.Kontrakty;

namespace egib
{
    public class DzialkaEwidencyjna : ObiektPowierzchniowy, IEnumerable<Klasouzytek>
    {
        private JednostkaRejestrowa _jednostkaRejestrowa;
        private DzialkaEwidencyjna _dzialkaDotychczasowa;
        private List<DzialkaEwidencyjna> _dzialkiPodzielone = new List<DzialkaEwidencyjna>();
        private string _przyczynaPodzialu;
        private IdentyfikatorDzialki _identyfikator;
        private List<Klasouzytek> _klasouzytki = new List<Klasouzytek>();
        private List<PunktGraniczny> _punkty = new List<PunktGraniczny>();
        private bool _doMetra = true;

        public DzialkaEwidencyjna(IdentyfikatorDzialki idDzialki, Powierzchnia powDzialki)
            : base(powDzialki)
        {
            Kontrakt.requires(idDzialki != null);
            Kontrakt.requires(powDzialki != null);
            _identyfikator = idDzialki;
            Kontrakt.ensures(_identyfikator == idDzialki);
            Kontrakt.ensures(powierzchnia().Equals(powDzialki));
            Kontrakt.ensures(_dzialkaDotychczasowa == null);
            Kontrakt.ensures(_dzialkiPodzielone.Count == 0);
        }

        public bool doMetra() { return _doMetra; }
        public void doMetra(bool m2) { _doMetra = m2; }

        public bool usunNieduzeUzytkiNiezabudowane(long ponizej = 51)
        {
            List<Klasouzytek> zostawioneUzytki = new List<Klasouzytek>();
            List<Klasouzytek> usunieteUzytki = new List<Klasouzytek>();
            long powNowa = 0;
            foreach (var uzytek in _klasouzytki)
            {
                long powUzytku = uzytek.powierzchnia().metryKwadratowe();
                if (!uzytek.jestZabudowany() && powUzytku < ponizej)
                {
                    usunieteUzytki.Add(uzytek);
                }
                else
                {
                    zostawioneUzytki.Add(uzytek);
                    powNowa += powUzytku;
                }
            }
            //nowa powierzchnia działki i nowe użytki
            if (zostawioneUzytki.Count == 0) return false; //musi zostać co najmniej jeden użytek
            foreach (var uzytek in usunieteUzytki)
            {
                long powUzytku = uzytek.powierzchnia().metryKwadratowe();
                Console.WriteLine("Ostrzeżenie: {0} usuwanie niezabudowanego, niedużego użytku: {1}, {2}", 
                    identyfikator(), uzytek, powUzytku);
            }
            _klasouzytki = zostawioneUzytki;
            powierzchnia(new Powierzchnia(powNowa));
            return true;
        }

        public JednostkaRejestrowa jednostkaRejestrowa() { return _jednostkaRejestrowa; }
        public void jednostkaRejestrowa(JednostkaRejestrowa jr)
        {
            Kontrakt.requires(jr != null);
            _jednostkaRejestrowa = jr;
        }

        public IdentyfikatorDzialki identyfikator()
        {
            return _identyfikator;
        }

        public string uwaga() { return _przyczynaPodzialu; }
        public void uwaga(string opis) { _przyczynaPodzialu = opis; }

        public bool przypisanaDotychczasowa()
        {
            return _dzialkaDotychczasowa != null;
        }

        private Powierzchnia powierzchniaDotychczasowa()
        {
            Kontrakt.requires(_dzialkaDotychczasowa != null, 
                "Działka nie ma przypisanej działki dotychczasowej " + _identyfikator);
            return _dzialkaDotychczasowa.powierzchnia();
        }

        public DzialkaEwidencyjna dzialkaDotychczasowa() { return _dzialkaDotychczasowa; }

        public void dzialkaDotychczasowa(DzialkaEwidencyjna dzialka)
        {
            Kontrakt.requires(_dzialkaDotychczasowa == null, 
                "Działce już została przypisana działka dotychczasowa " + _identyfikator);
            Kontrakt.requires(dzialka.jednostkaRejestrowa() != null, "Działka dotychczasowa nie ma przypisanej jednostki rejestrowej.");
            Kontrakt.requires(dzialka.countKlasouzytki() > 0, "Działka dotychczasowa nie ma żadnych użytków.");
            _dzialkaDotychczasowa = dzialka;
        }

        public void resetEwidencja(DzialkaEwidencyjna dzialka)
        {
            Kontrakt.requires(dzialka._dzialkiPodzielone.Count == 0);
            Kontrakt.requires(dzialka.jednostkaRejestrowa() != null, "Działka ewidencyjna nie ma przypisanej jednostki rejestrowej.");
            Kontrakt.requires(dzialka.countKlasouzytki() > 0, "Działka ewidencyjna nie ma żadnych użytków.");
            _dzialkaDotychczasowa = dzialka;
        }

        public void dodajPodzial(DzialkaEwidencyjna dzialkaPierwotna, string uwaga)
        {
            //Działka powinna mieć inny numer!!!
            Kontrakt.requires(przypisanaDotychczasowa(),
                "Nie można dodać podziału bez przypisania działki dotychczasowej.");
            Kontrakt.requires(_dzialkaDotychczasowa.Equals(dzialkaPierwotna), 
                "Podział działki dotyczy innej działki dotychczasowej " + _identyfikator);
            _dzialkaDotychczasowa.podziel(this);
            _dzialkaDotychczasowa._przyczynaPodzialu = uwaga;
        }

        private void podziel(DzialkaEwidencyjna dzialkaNowa)
        {
            Kontrakt.requires(_dzialkaDotychczasowa == null, "Działka ewidencyjna nie jest działką dotychczasową.");
            _dzialkiPodzielone.Add(dzialkaNowa);
        }
        
        public int countPodzielone()
        {
            Kontrakt.requires(_dzialkaDotychczasowa == null, "Działka ewidencyjna nie jest działką dotychczasową.");
            return _dzialkiPodzielone.Count;
        }

        public IEnumerable<DzialkaEwidencyjna> dzialkiPodzielone()
        {
            Kontrakt.requires(_dzialkaDotychczasowa == null, "Działka ewidencyjna nie jest działką dotychczasową.");
            return _dzialkiPodzielone;
        }

        public Powierzchnia powierzchniaUzytkow()
        {
            return new Powierzchnia(powierzchniaUzytkow(_klasouzytki));
        }

        private long powierzchniaUzytkow(IEnumerable<Klasouzytek> uzytki)
        {
            long m2Suma = 0;
            foreach (var uzytek in uzytki)
            {
                m2Suma += uzytek.powierzchnia().metryKwadratowe();
            }
            return m2Suma;
        }

        public void dodajKlasouzytek(Klasouzytek klasouzytek)
        {
            Kontrakt.requires(klasouzytek != null);
            int countPrzed = countKlasouzytki();
            foreach(var uzytek in _klasouzytki)
            {
                Kontrakt.assert(!uzytek.Equals(klasouzytek), "W działce " + identyfikator().ToString() + " już jest klasoużytek " + klasouzytek);
            }
            _klasouzytki.Add(klasouzytek);
            int countPo = countKlasouzytki();
            Kontrakt.ensures(countPrzed + 1 == countPo);
        }

        public int countKlasouzytki()
        {
            return _klasouzytki.Count;
        }

        public IEnumerator<Klasouzytek> GetEnumerator()
        {
            return _klasouzytki.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Klasouzytek[] uzytkiNiezabudowane()
        {
            List<Klasouzytek> uzytki = new List<Klasouzytek>();
            foreach (var uzytek in this) if (!uzytek.jestZabudowany()) uzytki.Add(uzytek);
            return uzytki.ToArray();
        }

        public Klasouzytek[] uzytkiZabudowane()
        {
            List<Klasouzytek> uzytki = new List<Klasouzytek>();
            foreach (var uzytek in this) if (uzytek.jestZabudowany()) uzytki.Add(uzytek);
            return uzytki.ToArray();
        }

        public Klasouzytek[] uzytkiLs()
        {
            List<Klasouzytek> uzytki = new List<Klasouzytek>();
            foreach (var uzytek in this) if (uzytek.jestLs()) uzytki.Add(uzytek);
            return uzytki.ToArray();
        }

        public IEnumerable<PunktGraniczny> punkty() { return _punkty; }

        public void dodajPunkt(PunktGraniczny punkt)
        {
            Kontrakt.requires(punkt != null);
            int countPrzed = countPunkty();
            _punkty.Add(punkt);
            int countPo = countPunkty();
            Kontrakt.ensures(countPrzed + 1 == countPo);
        }

        public int countPunkty()
        {
            return _punkty.Count;
        }
    }
}
