using System;
using System.Collections.Generic;

using swde.Rekordy;
using swde.Komponenty;

namespace swde.Geometria
{
    internal class MultiLiniaSwde : GeometriaSwde
    {
        private List<LiniaSwde> _linie;

        internal MultiLiniaSwde(RekordLiniowySwde rekord, bool geod)
        {
            _linie = new List<LiniaSwde>();

            foreach (RekordLiniaSwde linia in rekord.Linie)
            {
                _linie.Add(new LiniaSwde(linia, geod));
            }
        }

        /// <summary>
        /// Konwertuje multilinię na postać WKT.
        /// </summary>
        /// <remarks>
        /// Jeżeli jest tylko jedna linia, tworzy linie zamiast multilinii.
        /// np. MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10))
        /// np. MULTILINESTRING EMPTY
        /// </remarks>
        /// <param name="trzeciWymiar"></param>
        /// <returns></returns>
        public override string NaWkt(bool trzeciWymiar = false)
        {
            switch (_linie.Count)
            {
                case 0:
                    return "MULTILINESTRING EMPTY";
                case 1:
                    return _linie[0].NaWkt();
                default:
                    return string.Format("MULTILINESTRING {0}", this);
            }
        }

        public override string ToString()
        {
            return string.Format("({0})", string.Join(", ", _linie));
        }
    }
}
