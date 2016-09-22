using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Pragmatic.Kontrakty;

namespace egib
{
    public class Rozliczenie : IEnumerable<DzialkaEwidencyjna>
    {
        private string _jewTeryt;
        private string _jewNazwa;
        private Dictionary<string, DzialkaEwidencyjna> _indeks = new Dictionary<string, DzialkaEwidencyjna>();
        private List<DzialkaEwidencyjna> _dzialki = new List<DzialkaEwidencyjna>();
        private RodzajDzialki _rodzajDzialki;
        FabrykaKlasouzytku _klu;

        public Rozliczenie(RodzajDzialki rodzajDzialki, FabrykaKlasouzytku klu)
        {
            _rodzajDzialki = rodzajDzialki;
            _klu = klu;
        }

        public FabrykaKlasouzytku klu() { return _klu; }

        public void scal(Rozliczenie rozliczenie)
        {
            foreach(var dzialka in rozliczenie)
            {
                dodajDzialka(dzialka);
            }
        }

        public RodzajDzialki rodzaj { get { return _rodzajDzialki; } }
        public string terytJednostki() { return _jewTeryt; }
        public string nazwaJednostki() { return _jewNazwa; }

        public void jew(string teryt, string nazwa)
        {
            _jewTeryt = teryt;
            _jewNazwa = nazwa;
        }

        public void dodajDzialka(DzialkaEwidencyjna dzialka)
        {
            Kontrakt.requires(dzialka != null);
            Kontrakt.requires(dzialka.countKlasouzytki() > 0, "Działka nie zawiera żadnych klasoużytków.");
            Kontrakt.requires(!zawieraId(dzialka.identyfikator()), "Rozliczenie zawiera już działkę " + dzialka.identyfikator());
            int dzialkiPrzed = _dzialki.Count;
            _dzialki.Add(dzialka);
            IdentyfikatorDzialki id = dzialka.identyfikator();
            _indeks.Add(id.identyfikator(), dzialka);
            int dzialkiPo = _dzialki.Count;
            Kontrakt.ensures(dzialkiPrzed + 1 == dzialkiPo);
            Kontrakt.ensures(zawieraId(id));
        }

        /// <summary>
        /// Działki dla których nie ustalono rodzaju.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> nieustalonyRodzajDzialki()
        {
            return from dzialka in _dzialki where _rodzajDzialki.nieznany(dzialka) select dzialka.identyfikator().ToString();
        }
        
        public IEnumerable<DzialkaEwidencyjna> nieznaneDzialki()
        {
            return from dzialka in _dzialki where _rodzajDzialki.nieznany(dzialka) select dzialka;
        }

        public IEnumerable<DzialkaEwidencyjna> niepomierzoneDzialki()
        {
            return from dzialka in _dzialki where _rodzajDzialki.niepomierzona(dzialka) select dzialka;
        }

        public IEnumerable<DzialkaEwidencyjna> pomierzoneDzialki()
        {
            return from dzialka in _dzialki where _rodzajDzialki.pomierzona(dzialka) select dzialka;
        }

        public IEnumerable<DzialkaEwidencyjna> pomierzonePonizejOdchylkiDzialki()
        {
            return from dzialka in pomierzoneDzialki() where _rodzajDzialki.pomierzonaPonizejOdchylki(dzialka) select dzialka;
        }

        public IEnumerable<DzialkaEwidencyjna> pomierzoneInneDzialki()
        {
            return from dzialka in pomierzoneDzialki() where !_rodzajDzialki.pomierzonaPonizejOdchylki(dzialka) select dzialka;
        }

        public bool zawieraId(IdentyfikatorDzialki id)
        {
            return _indeks.ContainsKey(id.identyfikator());
        }

        public DzialkaEwidencyjna dzialkaById(IdentyfikatorDzialki id)
        {
            Kontrakt.requires(id != null);
            Kontrakt.requires(zawieraId(id), "Rozliczenie nie zawiera działki " + id.ToString());
            DzialkaEwidencyjna dzialka = _indeks[id.identyfikator()];
            Kontrakt.ensures(dzialka != null);
            return dzialka;
        }

        public DzialkaEwidencyjna dzialkaByIndex(int index)
        {
            return _dzialki[index];
        }

        public IEnumerator<DzialkaEwidencyjna> GetEnumerator()
        {
            return _dzialki.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<DzialkaEwidencyjna> dzialkiDotychczasowePodzielone()
        {
            Dictionary<string, DzialkaEwidencyjna> podzieloneDzialki = new Dictionary<string, DzialkaEwidencyjna>();
            List<DzialkaEwidencyjna> posortowaneDzialki = new List<DzialkaEwidencyjna>();
            foreach (var dzialka in this)
            {
                //Pomiń działki bez zdefiniowanej działki ewidencyjnej.
                if (!dzialka.przypisanaDotychczasowa()) continue;
                DzialkaEwidencyjna dzialkaDotychczasowa = dzialka.dzialkaDotychczasowa();
                //Pomiń działki niepodzielone.
                if (dzialkaDotychczasowa.countPodzielone() == 0) continue;
                //Działka nowa powinna mieć inny identyfikator niż działka dotychczasowa!
                Kontrakt.assert(!dzialka.identyfikator().Equals(dzialkaDotychczasowa.identyfikator()),
                    "Identyfikator nowej działki jest taki sam jak dotychczasowej.");
                string idPodzielona = dzialkaDotychczasowa.identyfikator().ToString();
                if (!podzieloneDzialki.ContainsKey(idPodzielona))
                {
                    podzieloneDzialki.Add(idPodzielona, dzialkaDotychczasowa);
                    posortowaneDzialki.Add(dzialkaDotychczasowa);
                }
            }
            return posortowaneDzialki;
        }

        public IEnumerable<DzialkaEwidencyjna> dzialkiDotychczasowe()
        {
            Dictionary<string, DzialkaEwidencyjna> dotychczasoweDzialki = new Dictionary<string, DzialkaEwidencyjna>();
            HashSet<string> pominieteDzialki = new HashSet<string>();
            foreach (var dzialka in this)
            {
                if (!dzialka.przypisanaDotychczasowa()) //Pomiń działki bez zdefiniowanej działki dotychczasowej.
                {
                    string id = dzialka.identyfikator().ToString();
                    pominieteDzialki.Add(id);
                }
                else //Dodaj unikatowe działki (działki podzielone mogą się powtarzać)
                {
                    DzialkaEwidencyjna dotychczasowa = dzialka.dzialkaDotychczasowa();
                    string id = dotychczasowa.identyfikator().ToString();
                    if (!dotychczasoweDzialki.ContainsKey(id)) dotychczasoweDzialki.Add(id, dotychczasowa);
                }
            }
            return dotychczasoweDzialki.Values;
        }

        /// <summary>
        /// Różnica powierzchni użytków i działki.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> rozbieznaPowierzchniaDzialki()
        {
            var query =
                from dzialka in _dzialki
                where !dzialka.powierzchnia().Equals(dzialka.powierzchniaUzytkow())
                select dzialka.identyfikator().ToString();
            return query;
        }

        /// <summary>
        /// Działki graficzne bez opisu.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> brakOpisuDzialki()
        {
            var query =
                from dzialka in _dzialki
                where !dzialka.przypisanaDotychczasowa()
                select dzialka.identyfikator().ToString();
            return query;
        }

    }
}
