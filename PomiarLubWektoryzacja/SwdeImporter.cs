using System.Collections.Generic;
using Pragmatic.Kontrakty;
using egib.swde;
using egib;

namespace egib.PomiarLubWektoryzacja
{
    public class SwdeImporter
    {
        DokumentSwde _dokument;
        //List<string> _działki = new List<string>();

        public SwdeImporter(string fileName) { _dokument = new DokumentSwde(fileName); }

        public IEnumerable<DzialkaEwidencyjna> działki()
        {
            var powierzchnia = new Powierzchnia(1);
            foreach (var dze in _dokument.GetObiektyKlasy("G5DZE"))
            {
                var g5idd = dze.GetAtrybut("G5IDD");
                var id = IdentyfikatorDzialki.parseG5(g5idd);
                var działka = new DzialkaEwidencyjna(id, powierzchnia);
                //Nie dodawaj do działki punktów jeżeli choć jeden jest niegraniczny, aby wymusić status "nieznany" dla działki.
                if (dze.Geometria.countPunktyNiegraniczne() == 0)
                {
                    Kontrakt.assert(działka.countPunkty() == 0, "Do działki były już importowane punkty.");
                    ObiektSwde[] punktySwde = dze.Geometria.PunktyGraniczne;
                    foreach (var punktSwde in punktySwde)
                    {
                        string g5zrd = punktSwde.GetAtrybut("G5ZRD");
                        string g5bpp = punktSwde.GetAtrybut("G5BPP");
                        var punkt = new PunktGraniczny(g5zrd, g5bpp);
                        ObiektSwde operat = punktSwde.GetRelacja("G5RKRG");
                        string g5syg = operat.GetAtrybut("G5SYG");
                        punkt.operat(g5syg);
                        działka.dodajPunkt(punkt);
                    }
                }
                yield return działka;
            }
        }
    }
}
