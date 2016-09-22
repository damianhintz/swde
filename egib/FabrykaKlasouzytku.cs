using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pragmatic.Kontrakty;

namespace egib
{
    /// <summary>
    /// Konwersja klasoużytku na oznaczenie i odwrotnie na podstawie słownika G5 (ozn) <-> (ofu,ozu,ozk).
    /// </summary>
    public class FabrykaKlasouzytku
    {
        private Dictionary<string, string[]> _oznaczenia = new Dictionary<string, string[]>();
        private Dictionary<string, string> _klu = new Dictionary<string, string>();

        public string[] map(string oznaczenie)
        {
            //Kontrakt.requires(_oznaczenia.ContainsKey(ozn), "Brak mapowania oznaczenia klasoużytku: " + ozn);
            string ozn = oznaczenie.Replace("/", "-");
            if (_oznaczenia.ContainsKey(ozn)) return _oznaczenia[ozn];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ostrzeżenie: nierozpoznane oznaczenie: zastosowano mapowanie {0} -> ({1},,)", oznaczenie, ozn);
            Console.ResetColor();
            return new string[] { ozn, "", "" };
        }

        public string map(string ofu, string ozu, string ozk)
        {
            //Kontrakt.requires(_klu.ContainsKey(klu), "Brak mapowania klasoużytku na oznaczenie: " + klu);
            string klu = join(ofu, ozu, ozk);
            if (_klu.ContainsKey(klu))
            {
                return oznEkologiczny(_klu[klu].Replace("-", "/"));
            }
            string ozn = oznEkologiczny((ofu + ozu + ozk).Replace("-", "/"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ostrzeżenie: nierozpoznany klasoużytek: zastosowano mapowanie {0} -> {1}", klu, ozn);
            Console.ResetColor();
            return ozn;
        }

        string oznEkologiczny(string ozn)
        {
            return ozn;
            //if (ozn.StartsWith("E/")) return "E-" + ozn.Substring(2);
            //else return ozn;
        }

        public void read(string fileName)
        {
            //OZN,OFU,OZU,OZK,OPIS,OFUst,OZUst,OZKst,
            //"B","B",,,"Tereny mieszkaniowe",,"B",,
            string[] lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            string header = lines.First();
            Kontrakt.assert(header.StartsWith("OZN,OFU,OZU,OZK,OPIS"));
            var query = from line in lines.Skip(1)
                        let split = line.Replace("\"", "").Replace("\'", "").Split(',')
                        select new
                        {
                            ozn = split[0],
                            ofu = split[1],
                            ozu = split[2],
                            ozk = split[3]
                        };
            foreach (var tuple in query)
            {
                string[] ooo = new string[] { tuple.ofu, tuple.ozu, tuple.ozk };
                _oznaczenia.Add(tuple.ozn, ooo);
                _klu.Add(join(ooo), tuple.ozn);
            }
        }

        private string join(params string[] ooo) { return "(" + string.Join(",", ooo) + ")"; }
    }
}
