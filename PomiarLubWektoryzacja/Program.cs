using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PomiarLubWektoryzacja
{
    class Program
    {
        string _fileName;

        private Program(string fileName) { _fileName = fileName; }

        static void Main(string[] args)
        {
            Program p = new Program(args[0]);
            p.printLogo();
            p.printKoniec();
        }

        private void printLogo()
        {
            Console.WriteLine("egib.pomiar v1.0 - Wykaz rodzaju działek (pomiar lub wektoryzacja)");
            Console.WriteLine("Copyright (c) 2015 OPGK Olsztyn. Wszelkie prawa zastrzeżone.");
            Console.WriteLine("Data publikacji: 19 stycznia 2015");
            Console.WriteLine("Plik SWDE: {0}", _fileName);
            var importer = new SwdeImporter(_fileName);
            var działki = importer.działki();
            var count = działki.Count();
            Console.WriteLine("Działki (G5DZE): {0}", count);
            WykazRodzaju wykaz = new WykazRodzaju(działki);
            //var ext = Path.GetExtension(_fileName);
            var file = Path.ChangeExtension(_fileName, "txt");
            Console.WriteLine("Wykaz rodzaju: {0}", file);
            wykaz.zapisz(file);
        }

        private void printKoniec()
        {
            Console.WriteLine("Koniec.");
            Console.Read();
        }
    }
}
