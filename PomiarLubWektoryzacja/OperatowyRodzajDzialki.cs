using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pragmatic.Kontrakty;
using egib;

namespace PomiarLubWektoryzacja
{
    public class OperatowyRodzajDziałki : RodzajDziałki
    {
        HashSet<string> operaty;
        
        public OperatowyRodzajDziałki(IEnumerable<string> operaty)
        {
            this.operaty = new HashSet<string>(operaty);
        }

        public override bool pomierzona(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            if (!pomierzone.HasValue) return false; //nieznana
            if (!pomierzone.Value) return false; //wektoryzacja
            if (operatowePunkty(dzialka)) return true; //pomiar
            if (aryPowierzchnia(dzialka)) return true; //pomiar
            return false; //wektoryzacja
        }

        public override bool niepomierzona(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            if (!pomierzone.HasValue) return false; //nieznana
            if (!pomierzone.Value) return true; //wektoryzacja
            if (operatowePunkty(dzialka)) return false; //pomiar
            if (aryPowierzchnia(dzialka)) return false; //pomiar
            return true; //wektoryzacja
        }

        public override bool nieznany(DzialkaEwidencyjna dzialka)
        {
            bool? pomierzone = pomierzonePunkty(dzialka);
            return !pomierzone.HasValue;
        }

        private bool operatowePunkty(DzialkaEwidencyjna dzialka)
        {
            foreach (var punkt in dzialka.punkty())
            {
                Kontrakt.assert(punkt.rodzaj().zPomiaru());
                string operat = punkt.operat();
                if (!operaty.Contains(operat)) return false;
            }
            return true;
        }

        private bool aryPowierzchnia(DzialkaEwidencyjna dzialka)
        {
            if (!dzialka.przypisanaDotychczasowa()) return false; //Brak działki ewidencyjnej
            return dzialka.dzialkaDotychczasowa().powierzchnia().metryKwadratowe().ToString().EndsWith("00");
        }
    }
}
