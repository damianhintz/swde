using System;

using egib.swde.Rekordy;
using egib.swde.Komponenty;

namespace egib.swde.Geometria
{
    internal class PunktSwde : GeometriaSwde
    {
        //public static bool Geodezyjny = true;
        private string _x;
        private string _y;
        private string _z = "0.0";

        internal PunktSwde(string x, string y, bool geod)
        {
            if (geod)
            {
                _x = y;
                _y = x;
            }
            else
            {
                _x = x;
                _y = y;
            }
            //_z = z;
        }

        internal PunktSwde(PozycjaSwde pozycja, bool geod)
            : this(pozycja.X, pozycja.Y, geod)
        {
        }

        internal PunktSwde(RekordPunktowySwde rekord, bool geod)
            : this(rekord.Pozycja, geod)
        {
        }

        /// <summary>
        /// Konwertuj punkt na postać WKT.
        /// </summary>
        /// <remarks>
        /// np. POINT (30 10), POINT EMPTY, POINT Z (1 1 80)
        /// </remarks>
        /// <param name="trzeciWymiar"></param>
        /// <returns></returns>
        public override string NaWkt(bool trzeciWymiar = false)
        {
            if (string.IsNullOrEmpty(_x)) return null;
            if (string.IsNullOrEmpty(_y)) return null;
            return string.Format("POINT ({0})", this);
        }

        public override string NaPunktWkt()
        {
            return NaWkt();
        }

        private string NaWkt3d()
        {
            return string.Format("POINT Z ({0} {1})", this, _z);
        }

        public override string ToString()
        {
            return string.Format("{0:F2} {1:F2}", _x, _y);
        }
    }
}
