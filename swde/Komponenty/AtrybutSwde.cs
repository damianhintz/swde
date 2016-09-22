namespace swde.Komponenty
{
    /// <summary>
    /// Atrybut rekordu.
    /// </summary>
    /// <remarks>
    /// Format SWDE umozliwia reprezentacje pól wielowartosciowych.
    /// Wszystkie kody atrybutów i relacji obiektów ewidencyjnych poprzedzone są przedrostekiem G5.
    /// W ramach typu rekordu pole atrybutu ma nadana nazwe i krotnosc. 
    /// </remarks>
    internal class AtrybutSwde : KomponentBase
    {
        protected string _pole;

        /// <summary>
        /// Nazwa/kod atrybutu.
        /// </summary>
        public string Pole { get { return _pole; } }

        protected string _napis;

        /// <summary>
        /// Wartość atrybutu.
        /// </summary>
        public string Napis { get { return _napis; } }

        public AtrybutSwde(string pole, string napis)
        {
            _pole = pole;
            _napis = napis;
        }

        public static explicit operator string(AtrybutSwde atrybut)
        {
            return atrybut.Napis;
        }
    }
}
