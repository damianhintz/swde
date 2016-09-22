using System.Collections.Generic;

namespace egib.swde.Komponenty
{
    /// <summary>
    /// Komponent rekordu liniowego.
    /// </summary>
    /// <remarks>
    /// Co najmniej jeden segment (2 punkty).
    /// Elementy opisu przestrzennego rekordu: 
    /// obszary składowe, linie składowe, punkty oparcia linii (wierzchołki) 
    /// mogą posiadać własne identyfikatory złożone z kodu i nazwy (nazwa może być pusta).
    /// Jeżeli nazwa nie jest pusta to wraz z kodem powinna być niepowtarzalna w ramach rekordu.
    /// Identyfikacja elementów umożliwia określenie roli jaką pełnią w opisie reprezentowanego obiektu.
    /// </remarks>
    internal class RekordLiniaSwde : KomponentBase
    {
        private List<PozycjaSwde> _segmenty;

        /// <summary>
        /// Zwraca kolekcję punktów składających się na linię.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Jeżeli nie zawiera minimalnej liczby punktów (obszar musi zawierać węzeł końcowy).
        /// </exception>
        public IEnumerable<PozycjaSwde> Segmenty
        {
            get
            {
                ZapewnijSegmenty();
                return _segmenty;
            }
        }

        /// <summary>
        /// Czy wystąpił węzeł końcowy.
        /// </summary>
        protected bool? _wezelKoncowy;

        public RekordLiniaSwde()
        {
            _segmenty = new List<PozycjaSwde>();
        }

        protected void ZapewnijMinimalneSegmenty(int minimalCount = 2)
        {
            KontrolerKontekstu.Zapewnij(_segmenty.Count >= minimalCount,
                string.Format("Linia lub obszar musi składać się z co najmniej {0} segmentów.", minimalCount));
        }

        protected virtual void ZapewnijSegmenty()
        {
            ZapewnijMinimalneSegmenty();
        }

        /// <summary>
        /// Dodaj kolejny punkt do linii.
        /// </summary>
        /// <param name="komponent"></param>
        public override void DodajPozycja(PozycjaSwde komponent)
        {
            _segmenty.Add(komponent);
        }

        private void ZapewnijTylkoJedenWezelKoncowy()
        {
            KontrolerKontekstu.Zapewnij(!_wezelKoncowy.HasValue, "Węzeł końcowy można dodać tylko raz.");
        }

        /// <summary>
        /// Zapewnij, aby dodać węzeł końcowy, musi być już jakaś pozycja.
        /// </summary>
        private void ZapewnijCoNajmniejJeden()
        {
            KontrolerKontekstu.Zapewnij(_segmenty.Count > 0, "Nie można dodać wezła końcowego jeżeli nie ma punktu początkowego.");
        }

        /// <summary>
        /// Dodaj ostatni punkt do linii.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Jeżeli nie zawiera punktów lub węzeł końcowy został już dodany.</exception>
        public override void DodajWezelKoncowy()
        {
            ZapewnijTylkoJedenWezelKoncowy();
            ZapewnijCoNajmniejJeden();
            _segmenty.Add(_segmenty[0]);
            _wezelKoncowy = true;
        }
    }
}
