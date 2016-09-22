using System;
using System.Collections.Generic;

using swde.Rekordy;
using swde.Komponenty;

namespace swde.Geometria
{
    internal class MultiObszarSwde : GeometriaSwde
    {
        private List<ObszarSwde> _obszary;

        internal MultiObszarSwde(RekordObszarowySwde rekord, bool geod)
        {
            _obszary = new List<ObszarSwde>();

            ObszarSwde aktualnyObszar = null;

            //Jeżeli komponent jest konturem zewnętrznym to tworzymy nowy obszar i dodajemy do listy obszarów.
            //Jeżeli komponent jest konturem wewnętrznym to dodajemy go do ostatniego obszaru.

            foreach (RekordObszarSwde obszar in rekord.Obszary)
            {
                if (obszar.KonturZewnetrzny)
                    _obszary.Add(aktualnyObszar = new ObszarSwde(obszar, geod));
                else
                {
                    //aktualnyObszar is not null
                    aktualnyObszar.DodajEnklawa(new LiniaSwde(obszar, geod));
                }
            }
        }

        /// <summary>
        /// Konwertuj multipoligon na postać WKT (upraszcza do poligonu jeżeli jest taka możliwość).
        /// </summary>
        /// <remarks>
        /// np. MULTIPOLYGON EMPTY
        /// np. MULTIPOLYGON (((30 20, 10 40, 45 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))
        /// np. MULTIPOLYGON (((40 40, 20 45, 45 30, 40 40)), ((20 35, 45 20, 30 5, 10 10, 10 30, 20 35), (30 20, 20 25, 20 15, 30 20)))
        /// </remarks>
        /// <returns></returns>
        public override string NaWkt(bool trzeciWymiar = false)
        {
            switch (_obszary.Count)
            {
                case 0:
                    return null;
                case 1:
                    return _obszary[0].NaWkt();
                default:
                    return string.Format("MULTIPOLYGON {0}", this);
            }
        }

        /// <summary>
        /// Bez upraszczania geometrii.
        /// </summary>
        /// <returns></returns>
        public override string NaMultiObszarWkt()
        {
            switch (_obszary.Count)
            {
                case 0: //Można tworzyć puste multipoligony.
                    return null;
                case 1: //Niezależnie czy jest tylko jeden poligon to tworzymy multipoligon.
                    return string.Format("MULTIPOLYGON {0}", this);
                default:
                    return string.Format("MULTIPOLYGON {0}", this);
            }
        }

        /// <summary>
        /// Upraszcza geometrię do poligonu jeżeli jest to możliwe.
        /// </summary>
        /// <returns></returns>
        public override string NaObszarWkt()
        {
            switch (_obszary.Count)
            {
                case 0: //Można tworzyć puste multipoligony.
                    return null;
                case 1: //Niezależnie czy jest tylko jeden poligon to tworzymy multipoligon.
                    return string.Format("MULTIPOLYGON {0}", this);
                default:
                    throw new ApplicationException("Multipoligonu składającego się z więcej niż jednego poligonu nie można zamienić na poligon.");
            }
        }

        public override string ToString()
        {
            return string.Format("({0})", string.Join(", ", _obszary));
        }
    }
}
