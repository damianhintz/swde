using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace egib
{
    public class EgbvRozliczenieWriter : RozliczenieWriter
    {
        private StreamWriter _writer;

        public EgbvRozliczenieWriter(Rozliczenie rozliczenie)
            : base(rozliczenie)
        {
        }

        public void writeNowe(string folderName, IEnumerable<DzialkaEwidencyjna> nowe)
        {
            //Podziel działki na obręby.
            Dictionary<string, List<DzialkaEwidencyjna>> dzialki = new Dictionary<string, List<DzialkaEwidencyjna>>();
            Dictionary<string, List<DzialkaEwidencyjna>> dzialkiPominiete = new Dictionary<string, List<DzialkaEwidencyjna>>();
            foreach (var dzialka in rozliczenie())
            {
                string obreb = dzialka.identyfikator().numerObrebu();
                if (!dzialki.ContainsKey(obreb))
                {
                    dzialki.Add(obreb, new List<DzialkaEwidencyjna>());
                    dzialkiPominiete.Add(obreb, new List<DzialkaEwidencyjna>());
                }
                if (dzialka.przypisanaDotychczasowa()) dzialki[obreb].Add(dzialka);
                else dzialkiPominiete[obreb].Add(dzialka);
            }
            foreach (var dzialka in nowe)
            {
                string obreb = dzialka.identyfikator().numerObrebu();
                if (!dzialki.ContainsKey(obreb))
                {
                    dzialki.Add(obreb, new List<DzialkaEwidencyjna>());
                    dzialkiPominiete.Add(obreb, new List<DzialkaEwidencyjna>());
                }
                dzialki[obreb].Add(dzialka);
            }
            foreach (var kv in dzialki)
            {
                string obreb = kv.Key;
                byte numerObrebu = byte.Parse(kv.Key);
                string name = numerObrebu.ToString("0000") + ".haklu";
                string fileName = Path.Combine(folderName, name);
                writeFile(fileName, dzialki[obreb]);
            }
        }

        public override void write(string folderName)
        {
            //Podziel działki na obręby.
            Dictionary<string, List<DzialkaEwidencyjna>> dzialki = new Dictionary<string, List<DzialkaEwidencyjna>>();
            Dictionary<string, List<DzialkaEwidencyjna>> dzialkiPominiete = new Dictionary<string, List<DzialkaEwidencyjna>>();
            foreach (var dzialka in rozliczenie())
            {
                string obreb = dzialka.identyfikator().numerObrebu();
                if (!dzialki.ContainsKey(obreb))
                {
                    dzialki.Add(obreb, new List<DzialkaEwidencyjna>());
                    dzialkiPominiete.Add(obreb, new List<DzialkaEwidencyjna>());
                }
                if (dzialka.przypisanaDotychczasowa()) dzialki[obreb].Add(dzialka);
                else dzialkiPominiete[obreb].Add(dzialka);
            }
            foreach (var kv in dzialki)
            {
                string obreb = kv.Key;
                byte numerObrebu = byte.Parse(kv.Key);
                string name = numerObrebu.ToString("0000") + ".haklu";
                string fileName = Path.Combine(folderName, name);
                writeFile(fileName, dzialki[obreb]);
            }
        }

        public void writeFile(string fileName, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            _writer = new StreamWriter(fileName, false, Encoding.GetEncoding(1250));
            writeHeader();
            writeDzialki(dzialki);
            _writer.Close();
        }

        private void writeHeader()
        {
            _writer.WriteLine("; Kontury klasyfikacyjne w działkach");
            //; Obręb: D:\egb5win\BAZA\142302_2 Gielniow\142302_2\0001\
            //Metadane dodatkowe:
            _writer.WriteLine("; Aplikacja: egbv.WykazZmian");
            _writer.WriteLine("; Plik źródłowy: *.m2klu");
            _writer.WriteLine("; Pochodzenie: Ewmapa");
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
            //- 296   1.5000
            _writer.WriteLine("- {0}   {1:F4}",
                    dzialka.identyfikator().numerDzialki(),
                    dzialka.powierzchnia().hektary());
            foreach (var uzytek in dzialka)
            {
                writeUzytek(uzytek);
            }
            _writer.WriteLine();
        }

        private void writeUzytek(Klasouzytek uzytek)
        {
            //    LsV           1.5000
            _writer.WriteLine("    {0,-14}{1:F4}",
                uzytek.oznaczenie(rozliczenie().klu()),
                uzytek.powierzchnia().hektary());
        }
    }
}
