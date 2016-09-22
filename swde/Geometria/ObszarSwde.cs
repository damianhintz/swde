using System.Collections.Generic;

using egib.swde.Komponenty;

namespace egib.swde.Geometria
{
    internal class ObszarSwde : LiniaSwde
    {
        private List<LiniaSwde> _enklawy;

        internal ObszarSwde(RekordObszarSwde rekord, bool geod) : base(rekord, geod)
        {
            _enklawy = new List<LiniaSwde>();
        }

        public void DodajEnklawa(LiniaSwde enklawa)
        {
            _enklawy.Add(enklawa);
        }

        /// <summary>
        /// Konwertuje poligon na postać WKT.
        /// </summary>
        /// <remarks>
        /// np. POLYGON EMPTY
        /// np. POLYGON ((30 10, 10 20, 20 40, 40 40, 30 10))
        /// np. POLYGON ((35 10, 10 20, 15 40, 45 45, 35 10), (20 30, 35 35, 30 20, 20 30))
        /// </remarks>
        /// <returns></returns>
        public override string NaWkt()
        {
            if (_punkty.Count == 0) return "POLYGON EMPTY";
            return string.Format("POLYGON {0}", this);
        }

        public override string ToString()
        {
            if (_enklawy.Count == 0) return string.Format("({0})", base.ToString());
            return string.Format("({0}, {1})", base.ToString(), string.Join(", ", _enklawy));
        }
    }
}
