using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologia
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Topologia v1.0.1 - Kontrola topologiczna konturów klasyfikacyjnych z SWDE (G5KKL)");
            Console.WriteLine("Copyright (c) 2014 OPGK Olsztyn. Wszelkie prawa zastrzeżone.");
            Console.WriteLine("Data publikacji: 21 listopada 2014");
            string dir = args[0].dir();
            Logger.inputPath(dir);
            SwdeReader stare = new SwdeReader(args[0]);
            Logger.writeEnd();
            Console.WriteLine("Koniec.");
            Console.Read();
        }
    }
}
