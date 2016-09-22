using System.Collections.Generic;

using egib.swde.Komponenty;

namespace egib.swde.Geometria
{
    internal class LiniaSwde
    {
        protected List<PunktSwde> _punkty;

        internal LiniaSwde(RekordLiniaSwde rekord, bool geod)
        {
            _punkty = new List<PunktSwde>();

            foreach (PozycjaSwde segment in rekord.Segmenty)
            {
                _punkty.Add(new PunktSwde(segment, geod));
            }
        }

        /// <summary>
        /// Konwertuje linię na postać WKT.
        /// </summary>
        /// <remarks>
        /// np. LINESTRING (30 10, 10 30, 40 40)
        /// np. LINESTRING EMPTY
        /// </remarks>
        /// <returns></returns>
        public virtual string NaWkt()
        {
            if (_punkty.Count == 0) return "LINESTRING EMPTY";
            return string.Format("LINESTRING {0}", this);
        }

        public override string ToString()
        {
            return string.Format("({0})", string.Join(", ", _punkty));
        }
    }
}
