using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pragmatic.Kontrakty;
using egib;

namespace PomiarLubWektoryzacja
{
    public class RodzajDziałki
    {
        public virtual bool pomierzona(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            if (!pomierzone.HasValue) return false; //nieznana
            return pomierzone.Value;
        }

        public virtual bool niepomierzona(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            if (!pomierzone.HasValue) return false; //nieznana
            return !pomierzone.Value;
        }

        public virtual bool nieznany(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            return !pomierzone.HasValue;
        }

        protected bool? pomierzonePunkty(DzialkaEwidencyjna dzialka)
        {
            List<PunktGraniczny> nieznane = new List<PunktGraniczny>();
            List<PunktGraniczny> operatowe = new List<PunktGraniczny>();
            List<PunktGraniczny> wektoryzacja = new List<PunktGraniczny>();
            int innePunkty = 0;
            foreach (var punkt in dzialka.punkty())
            {
                RodzajPunktu rodzaj = punkt.rodzaj();
                if (rodzaj.nieznany()) nieznane.Add(punkt);
                else if (rodzaj.zWektoryzacji()) wektoryzacja.Add(punkt);
                else if (rodzaj.zPomiaru()) operatowe.Add(punkt);
                else innePunkty++;
            }
            Kontrakt.assert(innePunkty == 0);
            if (nieznane.Count > 0) return null;
            if (wektoryzacja.Count > 0) return false;
            if (operatowe.Count > 0) return true;
            return null;
        }

        public bool pomierzonaPonizejOdchylki(DzialkaEwidencyjna dzialka)
        {
            if (!dzialka.przypisanaDotychczasowa()) return false; //Brak działki ewidencyjnej
            DzialkaEwidencyjna dotychczasowa = dzialka.dzialkaDotychczasowa();
            if (dotychczasowa.countPodzielone() > 0) return false; //Działka jest podzielona lub przenumerowana
            long pg = dzialka.powierzchnia().metryKwadratowe();
            long pe = dotychczasowa.powierzchnia().metryKwadratowe();
            if (pg == pe) return false; //Powierzchnia działek jest identyczna
            long dr = pg - pe; //Różnica powierzchni
            long drAbsolute = Math.Abs(dr);
            double dp = 0.0001 * pg + 0.2 * Math.Sqrt(pg); //Dopuszczalna odchyłka
            long dpRounded = (long)Math.Round(dp, MidpointRounding.ToEven);
            return drAbsolute == 1 || drAbsolute == 2 || drAbsolute == 3 || drAbsolute <= dpRounded;
        }
    }
}
