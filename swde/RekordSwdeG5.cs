using swde.Rekordy;

namespace swde
{
    /// <summary>
    /// Rekord ewidencyjny EGiB.
    /// </summary>
    internal abstract class RekordSwdeG5 : RekordSwdeBase
    {
        /// <summary>
        /// Prefiks typów i atrybutów.
        /// </summary>
        public static readonly string PrefiksG5 = "G5";

        private static readonly string PrefiksOpisowyG5 = "G5O_";
        private static readonly string PrefiksGrafikiG5 = "G5G_";

        public RekordSwdeG5(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
        }

        private bool DataDtwNieMozeBycNizszaNizDtu()
        {
            return false;
        }

        private bool DataDtwNieMozeBycPusta()
        {
            return false;
        }

        /// <summary>
        /// Normalizuje kod typu, zgodnie z notacją G5.
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public static string NormalizujPrefiksTypu(string typ)
        {
            if (string.IsNullOrEmpty(typ)) return typ;

            if (typ.StartsWith(PrefiksG5))
            {
                typ = typ.Replace(PrefiksOpisowyG5, PrefiksG5);
                typ = typ.Replace(PrefiksGrafikiG5, PrefiksG5);

                return typ;
            }

            return PrefiksG5 + typ;
        }

        /// <summary>
        /// Normalizuje kod atrybutu, zgodnie z notacją G5.
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        public static string NormalizujPrefiksAtrybutu(string kod)
        {
            if (string.IsNullOrEmpty(kod)) return kod;

            if (kod.StartsWith(PrefiksG5))
            {
                return kod;
            }

            return PrefiksG5 + kod;
        }
    }
}
