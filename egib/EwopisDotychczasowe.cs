using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace egib
{
    public class EwopisDotychczasowe : RozliczenieWriter
    {
        private StreamWriter _writer;
        
        public EwopisDotychczasowe(Rozliczenie rozliczenie) : base(rozliczenie) { }

        public void writeAll(string fileName)
        {
            //Pomiń działki bez części opisowej, aby przy imporcie nie było ostrzeżeń
            var dzialki = from dzialka in rozliczenie() where dzialka.przypisanaDotychczasowa() select dzialka;
            writeFile(fileName, dzialki);
        }

        public override void write(string folderName)
        {
            throw new NotImplementedException();
        }

        public void writeFile(string fileName, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            _writer = new StreamWriter(fileName, false, Encoding.GetEncoding(1250));
            writeDzialki(Path.GetFileName(fileName), dzialki);
            _writer.Close();
        }

        private void writeDzialki(string name, IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            foreach (var dzialka in dzialki) writeDzialka(dzialka.dzialkaDotychczasowa());
        }

        private void writeDzialka(DzialkaEwidencyjna dzialka)
        {
            _writer.WriteLine("{0,-15}{1,15}", dzialka.identyfikator(),
                    dzialka.powierzchnia().metryKwadratowe());
            foreach (var uzytek in dzialka) writeUzytek(uzytek);
            _writer.WriteLine("**");
        }

        private void writeUzytek(Klasouzytek uzytek)
        {
            string ofu = uzytek.ofu();
            string ozu = uzytek.ozu();
            string ozk = uzytek.ozk();
            string ozn = ozu + ozk;
            
            if (!string.IsNullOrEmpty(ofu))
            {
                if (string.IsNullOrEmpty(ozn)) ozn = ofu;
                else if (!ofu.Equals(ozu)) ozn = ofu + "/" + ozn;
            }

            _writer.WriteLine("{0,-15}{1,15}", ozn,
                uzytek.powierzchnia().metryKwadratowe());
        }
    }
}
