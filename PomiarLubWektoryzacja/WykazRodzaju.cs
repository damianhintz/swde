using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pragmatic.Kontrakty;
using egib;

namespace egib.PomiarLubWektoryzacja
{
    /// <summary>
    /// Wykaz rodzaju działek w rozliczeniu.
    /// </summary>
    class WykazRodzaju
    {
        StreamWriter _writer;
        List<DzialkaEwidencyjna> _działki;
        RodzajDzialki _rodzaj = new RodzajDzialki();

        public WykazRodzaju(IEnumerable<DzialkaEwidencyjna> działki) { _działki = new List<DzialkaEwidencyjna>(działki); }

        public void zapisz(string fileName)
        {
            _writer = new StreamWriter(fileName, false, Encoding.GetEncoding(1250));
            writeHeader();
            writeDziałki(_działki);
            _writer.Close();
        }

        private void writeHeader()
        {
            _writer.WriteLine("Wykaz rodzaju działek w rozliczeniu");
            _writer.WriteLine();
        }

        private void writeDziałki(IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            foreach (var dzialka in dzialki) writeDziałka(dzialka);
        }

        private void writeDziałka(DzialkaEwidencyjna dzialka)
        {
            string id = dzialka.identyfikator().ToString();
            bool nieznany = _rodzaj.nieznany(dzialka);
            bool wektoryzacja = _rodzaj.niepomierzona(dzialka);
            bool pomierzona = _rodzaj.pomierzona(dzialka);
            var operaty = from pkt in dzialka.punkty() 
                          select "[" + 
                          pkt.zrodloDanych() + ", " +
                          pkt.bladPolozenia() + "]";
            string joinOperaty = string.Join(" ", operaty);
            if (nieznany) _writer.WriteLine("{0,-16}{1,-32}{2}", id, "prawdopodobnie wektoryzacja", joinOperaty);
            if (wektoryzacja) _writer.WriteLine("{0,-16}{1,-32}{2}", id, "wektoryzacja", joinOperaty);
            if (pomierzona) _writer.WriteLine("{0,-16}{1,-32}{2}", id, "pomiar", joinOperaty);
            int count = 0;
            if (nieznany) count++;
            if (wektoryzacja) count++;
            if (pomierzona) count++;
            Kontrakt.ensures(count == 1, "Konflikt rodzaju działki: " + id);
        }
    }
}
