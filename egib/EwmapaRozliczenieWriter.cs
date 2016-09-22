using System.Collections.Generic;
using System.Text;
using System.IO;

namespace egib
{
    public class EwmapaRozliczenieWriter : RozliczenieWriter
    {
        private StreamWriter _writer;

        public EwmapaRozliczenieWriter(Rozliczenie rozliczenie)
            : base(rozliczenie)
        {
        }

        public override void write(string fileName)
        {
            writeFile(fileName, rozliczenie());
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
            _writer.WriteLine("Rozliczenie konturów klasyfikacyjnych na działkach [m^2]");
            _writer.WriteLine();
        }

        private void writeDzialki(IEnumerable<DzialkaEwidencyjna> dzialki)
        {
            //1-1                      19857
            //  dr                     19857
            //
            foreach (var dzialka in dzialki)
            {
                writeDzialka(dzialka);
            }
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
            _writer.WriteLine();
        }

        private void writeUzytek(Klasouzytek uzytek)
        {
            _writer.WriteLine("  {0,-13}{1,15}",
                uzytek.oznaczenie(rozliczenie().klu()),
                uzytek.powierzchnia().metryKwadratowe());
        }
    }
}
