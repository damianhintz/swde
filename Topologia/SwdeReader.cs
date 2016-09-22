using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;
//using Pragmatic.Kontrakty;
using swde;
using egib;
using GeoAPI.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.GML2;

namespace Topologia
{
    /// <summary>
    /// Klasoużytek (KKL: ofu,ozu,ozk,pew) -> Obręb (ROBR)
    /// </summary>
    public class SwdeReader
    {
        public SwdeReader(string fileName)
        {
            DokumentSwde swde = new DokumentSwde(fileName);
            int kontury = readKontury(swde);
            Console.WriteLine("Kontury klasyfikacyjne: {0}", kontury);
            Logger.write("Kontury klasyfikacyjne: " + kontury);
        }

        int readKontury(DokumentSwde swde)
        {
            List<IGeometry> geometrie = new List<IGeometry>();
            var kontury = swde.GetObiektyKlasy("G5KKL");
            foreach (var klu in kontury)
            {
                string g5idk = klu.GetAtrybut("G5IDK");
                string g5ozu = klu.GetAtrybut("G5OZU");
                string g5ofu = string.Empty;
                string g5ozk = klu.GetAtrybut("G5OZK");
                string g5pew = klu.GetAtrybut("G5PEW");
                Powierzchnia powierzchnia = Powierzchnia.parseMetry(g5pew);
                if (string.IsNullOrEmpty(g5ozk))
                {
                    //użytek
                    g5ofu = g5ozu;
                    g5ozu = string.Empty;
                }
                else
                {
                    //kontur klasyfikacyjny
                    g5ofu = g5ozu;
                    string[] split = g5ofu.Split(new char[] { '-' }, 3);
                    switch (split.Length)
                    {
                        case 2:
                            g5ofu = split[0];
                            g5ozu = split[1];
                            if (g5ofu.Equals("E")) //nie dziel E
                            {
                                g5ofu = split[0] + '-' + split[1];
                                g5ozu = split[1];
                            }
                        break;
                        case 3:
                            g5ofu = split[0] + "-" + split[1];
                            g5ozu = split[2];
                        break;
                        default: break;
                    }
                }
                string oznString = string.Format("{4}\t{0}\t{1}\t{2}\t{3}", g5ofu, g5ozu, g5ozk, g5pew, g5idk);
                Logger.write(oznString);
                EGB_OFU ofu = new EGB_OFU(g5ofu);
                EGB_OZU? ozu = null;
                if (!string.IsNullOrEmpty(g5ozu)) ozu = new EGB_OZU(g5ozu);
                EGB_OZK? ozk = null;
                if (!string.IsNullOrEmpty(g5ozk)) ozk = new EGB_OZK(g5ozk);
                EGB_OznaczenieKlasouzytku ozn = new EGB_OznaczenieKlasouzytku(ofu, ozu, ozk);
                ozn.walidujOgraniczenia();
                //Console.WriteLine(g5idk);
                string wkt = klu.Geometria.NaWkt();
                GeometriaObiektu geometria = new GeometriaObiektu(wkt);
                try
                {
                    geometria.overlaps(geometrie);
                }
                catch (Exception ex)
                {
                    Logger.writeBłąd(ex.Message + ":" + wkt);
                }
                geometrie.Add(geometria.geometry());
            }
            return kontury.Count();
        }
    }
}
