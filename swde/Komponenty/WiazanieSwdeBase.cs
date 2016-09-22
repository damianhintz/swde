using swde.Sekcje;

namespace swde.Komponenty
{
    /// <summary>
    /// Wiązanie jest zależnością łączącą klasy obiektów.
    /// Atrybuty zawierające wskazania na inne obiekty reprezentujące relacje.
    /// Wiązanie posiada krotność.
    /// </summary>
    internal abstract class WiazanieSwdeBase : ReferencjaSwde
    {
        protected string _pole;

        /// <summary>
        /// Kod relacji.
        /// </summary>
        public string Pole { get { return _pole; } }

        public WiazanieSwdeBase(SekcjaObiektowSwde obiekty, string pole)
            : base(obiekty)
        {
            _pole = pole;
        }
    }
}
