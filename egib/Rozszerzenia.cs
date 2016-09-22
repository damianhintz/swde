using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Pragmatic.Kontrakty;
using egib;

namespace egib
{
    public static class Rozszerzenia
    {
        public static bool zmienionaDzialka(this Rozliczenie rozliczenie, DzialkaEwidencyjna dzialkaDotychczasowa)
        {
            IdentyfikatorDzialki id = dzialkaDotychczasowa.identyfikator();
            DzialkaEwidencyjna dzialkaNowa = rozliczenie.dzialkaById(id);
            foreach (var klasouzytek in dzialkaNowa.unionUzytki(dzialkaDotychczasowa))
            {
                Klasouzytek stary = null;
                foreach (var uzytek in dzialkaDotychczasowa)
                {
                    if (klasouzytek.Equals(uzytek))
                    {
                        stary = uzytek;
                        break;
                    }
                }
                Klasouzytek nowy = null;
                foreach (var uzytek in dzialkaNowa)
                {
                    if (klasouzytek.Equals(uzytek))
                    {
                        nowy = uzytek;
                        break;
                    }
                }
                if (stary == null || nowy == null) return true; //Zmieniona, bo różnica użytków
                Powierzchnia stara = stary.powierzchnia();
                Powierzchnia nowa = nowy.powierzchnia();
                if (nowa.powyzejOdchylki(stara)) return true; //Zmieniona, bo powyżej odchyłki
            }
            return dzialkaNowa.powierzchnia().powyzejOdchylki(dzialkaDotychczasowa.powierzchnia());
        }
        
        public static IEnumerable<Klasouzytek> unionUzytki(this DzialkaEwidencyjna dzialkaNowa, DzialkaEwidencyjna dzialkaDotychczasowa)
        {
            HashSet<Klasouzytek> uzytki = new HashSet<Klasouzytek>();
            foreach (var uzytek in dzialkaDotychczasowa) uzytki.Add(uzytek);
            foreach (var uzytek in dzialkaNowa) uzytki.Add(uzytek);
            List<Klasouzytek> uzytkiPosortowane = new List<Klasouzytek>(uzytki);
            uzytkiPosortowane.Sort();
            return uzytkiPosortowane;
        }

        public static bool powyzejOdchylki(this Powierzchnia nowa, Powierzchnia dotychczasowa)
        {
            long pg = nowa.metryKwadratowe();
            long pe = dotychczasowa.metryKwadratowe();
            if (pg == pe) return false; //Powierzchnia jest identyczna
            long dr = pg - pe; //Różnica powierzchni
            long drAbsolute = Math.Abs(dr);
            double dp = 0.0001 * pg + 0.2 * Math.Sqrt(pg); //Dopuszczalna odchyłka
            long dpRounded = (long)Math.Round(dp, MidpointRounding.ToEven);
            if (drAbsolute <= 3) return false;
            return drAbsolute > dpRounded;
        }

        public static Dictionary<string, List<DzialkaEwidencyjna>> dzialkiDotychczasoweObrebami(this Rozliczenie rozliczenie)
        {
            return rozliczenie.dzialkiObrebami(rozliczenie.dzialkiDotychczasowe());
        }

        public static Dictionary<string, List<DzialkaEwidencyjna>> dzialkiObrebami(this Rozliczenie rozliczenie, 
            IEnumerable<DzialkaEwidencyjna> dzialkiList)
        {
            Dictionary<string, List<DzialkaEwidencyjna>> dzialki = new Dictionary<string, List<DzialkaEwidencyjna>>();
            foreach (var dzialka in dzialkiList)
            {
                string obreb = dzialka.identyfikator().numerObrebu();
                if (!dzialki.ContainsKey(obreb)) dzialki.Add(obreb, new List<DzialkaEwidencyjna>());
                dzialki[obreb].Add(dzialka);
            }
            return dzialki;
        }
    }
}
