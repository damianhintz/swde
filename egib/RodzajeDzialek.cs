using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pragmatic.Kontrakty;

namespace egib
{
    /// <summary>
    /// Wykaz rodzaju działek w rozliczeniu.
    /// </summary>
    public class RodzajeDzialek : RozliczenieWriter
    {
        private StreamWriter _writer;
        
        public RodzajeDzialek(Rozliczenie rozliczenie)
            : base(rozliczenie)
        {
        }

        public override void write(string fileName)
        {
            writeFile(fileName, rozliczenie());
        }

        private void writeFile(string fileName, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            _writer = new StreamWriter(fileName, false, Encoding.GetEncoding(1250));
            writeHeader();
            writeDzialki(dzialki);
            _writer.Close();
        }

        private void writeHeader()
        {
            _writer.WriteLine("Wykaz rodzaju działek w rozliczeniu");
            _writer.WriteLine();
        }

        private void writeDzialki(IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            foreach (var dzialka in dzialki)
            {
                writeDzialka(dzialka);
            }
        }

        private void writeDzialka(DzialkaEwidencyjna dzialka)
        {
            string id = dzialka.identyfikator().ToString();
            RodzajDzialki rodzaj = rozliczenie().rodzaj;
            bool nieznany = rodzaj.nieznany(dzialka);
            bool wektoryzacja = rodzaj.niepomierzona(dzialka);
            bool pomierzona = rodzaj.pomierzona(dzialka);
            var operaty = from pkt in dzialka.punkty() 
                          select "[" + 
                          pkt.zrodloDanych() + ", " +
                          pkt.bladPolozenia() + ", " + 
                          pkt.operat() + "]";
            string joinOperaty = string.Join(" ", operaty);
            if (nieznany) _writer.WriteLine("{0,-16}{1,-16}{2}", id, "nieznany", joinOperaty);
            if (wektoryzacja) _writer.WriteLine("{0,-16}{1,-16}{2}", id, "wektoryzacja", joinOperaty);
            if (pomierzona) _writer.WriteLine("{0,-16}{1,-16}{2}", id, "pomiar", joinOperaty);
            int count = 0;
            if (nieznany) count++;
            if (wektoryzacja) count++;
            if (pomierzona) count++;
            Kontrakt.ensures(count == 1, "Konflikt rodzaju działki: " + id);
        }
    }
}
