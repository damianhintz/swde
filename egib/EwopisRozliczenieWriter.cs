using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace egib
{
    public class EwopisRozliczenieWriter : RozliczenieWriter
    {
        private StreamWriter _writer;
        private List<string> pominiete = new List<string>();

        public EwopisRozliczenieWriter(Rozliczenie rozliczenie) : base(rozliczenie) { }

        public IEnumerable<string> niezmienioneDzialki() { return pominiete; }

        public void writeAll(string fileName)
        {
            //Pomiń działki bez części opisowej, aby przy imporcie nie było ostrzeżeń
            var dzialki = from dzialka in rozliczenie() where dzialka.przypisanaDotychczasowa() select dzialka;
            writeFile(fileName, dzialki);
        }

        public override void write(string folderName)
        {
            this.pominiete.Clear();
            Dictionary<string, List<DzialkaEwidencyjna>> dzialki = new Dictionary<string, List<DzialkaEwidencyjna>>();
            Dictionary<string, List<DzialkaEwidencyjna>> pominiete = new Dictionary<string, List<DzialkaEwidencyjna>>();
            foreach (var dzialka in rozliczenie())
            {
                string obreb = dzialka.identyfikator().numerObrebu();
                if (!dzialki.ContainsKey(obreb))
                {
                    dzialki.Add(obreb, new List<DzialkaEwidencyjna>());
                    pominiete.Add(obreb, new List<DzialkaEwidencyjna>());
                }
                //Pomiń działki bez części opisowej, aby przy imporcie nie było ostrzeżeń
                if (dzialka.przypisanaDotychczasowa()) dzialki[obreb].Add(dzialka);
                else pominiete[obreb].Add(dzialka);
            }
            foreach (var kv in dzialki)
            {
                string obreb = kv.Key;
                byte numerObrebu = byte.Parse(kv.Key);
                string name = numerObrebu.ToString("0000") + ".arklu";
                string fileName = Path.Combine(folderName, name);
                writeFile(fileName, dzialki[obreb]);
                if (pominiete[obreb].Count > 0) Console.WriteLine("{0}: {1} działki bez części opisowej (pominięte)", name, pominiete[obreb].Count);
            }
        }

        public void writeFile(string fileName, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            _writer = new StreamWriter(fileName, false, Encoding.GetEncoding(1250));
            writeDzialki(Path.GetFileName(fileName), dzialki);
            _writer.Close();
        }

        private void writeDzialki(string name, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            pominiete.Clear();
            foreach (var dzialka in dzialki)
            {
                //Pomiń działki niezmienione
                DzialkaEwidencyjna dzialkaDotychczasowa = dzialka.dzialkaDotychczasowa();
                Rozliczenie _rozliczenie = rozliczenie();
                if (!_rozliczenie.zmienionaDzialka(dzialkaDotychczasowa))
                {
                    pominiete.Add(dzialka.identyfikator().ToString());
                    continue;
                }
                writeDzialka(dzialka);
            }
            if (pominiete.Count > 0) Console.WriteLine("{1}: {0} działki niezmienione (pominięte)", pominiete.Count, name);
        }

        private void writeDzialka(DzialkaEwidencyjna dzialka)
        {
            _writer.WriteLine("{0,-15}{1,15}",
                    dzialka.identyfikator(),
                    dzialka.powierzchnia().metryKwadratowe());
            foreach (var uzytek in dzialka)
            {
                writeUzytek(uzytek);
            }
            _writer.WriteLine("**");
        }

        private void writeUzytek(Klasouzytek uzytek)
        {
            _writer.WriteLine("{0,-15}{1,15}",
                uzytek.oznaczenie(rozliczenie().klu()),
                uzytek.powierzchnia().metryKwadratowe());
        }
    }
}
