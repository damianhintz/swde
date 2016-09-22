namespace swde.Komponenty
{
    /// <summary>
    /// Komponent rekordu obszarowego.
    /// </summary>
    /// <remarks>
    /// Obszar też może być linią, tylko zamkniętą.
    /// </remarks>
    internal class RekordObszarSwde : RekordLiniaSwde
    {
        private bool? _konturZewnetrzny;

        /// <summary>
        /// Czy kontur jest zewnętrzny.
        /// Jeżeli kontur nie został ustawiony, to domyślnie jest zewnętrzny.
        /// </summary>
        public bool KonturZewnetrzny { get { return _konturZewnetrzny.GetValueOrDefault(true); } }

        public RekordObszarSwde()
        {
        }

        protected override void ZapewnijSegmenty()
        {
            base.ZapewnijSegmenty();
            ZapewnijMinimalneSegmenty(3);
            ZapewnijJedenWezelKoncowy();
        }

        private void ZapewnijJedenWezelKoncowy()
        {
            KontrolerKontekstu.Zapewnij(_wezelKoncowy.HasValue, "Obszar musi mieć węzeł końcowy.");
        }

        private void ZapewnijTylkoJedenKontur()
        {
            KontrolerKontekstu.Zapewnij(!_konturZewnetrzny.HasValue, "Kontur można ustawić tylko raz.");
        }
        
        /// <summary>
        /// Określ typ konturu na zewnętrzny.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Jeżeli typ konturu został już ustawiony.</exception>
        public override void DodajKonturZew()
        {
            ZapewnijTylkoJedenKontur();
            _konturZewnetrzny = true;
        }

        /// <summary>
        /// Określ typ konturu na wewnętrzny.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Jeżeli typ konturu został już ustawiony.</exception>
        public override void DodajKonturWew()
        {
            ZapewnijTylkoJedenKontur();
            _konturZewnetrzny = false;
        }
    }
}
