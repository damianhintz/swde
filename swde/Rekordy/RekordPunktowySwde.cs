using swde.Komponenty;

namespace swde.Rekordy
{
    /// <summary>
    /// Opis przestrzenny rekordu jest punktem.
    /// </summary>
    internal class RekordPunktowySwde : RekordSwdeBase
    {
        public override string TypBazowy
        {
            get
            {
                return "RP";
            }
        }

        private PozycjaSwde _pozycja;

        /// <summary>
        /// Zwraca pozycję rekordu.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Jeżeli pozycja jest pusta.</exception>
        public PozycjaSwde Pozycja
        {
            get
            {
                ZapewnijNotNull(_pozycja);
                return _pozycja;
            }
        }

        public RekordPunktowySwde(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
            _pozycja = null;
        }

        private void ZapewnijNotNull(PozycjaSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(komponent != null, "Pozycja rekordu punktowego nie może być pusta.");
        }

        private void ZapewnijTylkoJedenPunkt()
        {
            KontrolerKontekstu.Zapewnij(_pozycja == null, "Można dodać tylko jeden punkt do rekordu punktowego.");
        }

        /// <summary>
        /// Dodaj pozycję rekordu.
        /// </summary>
        /// <remarks>
        /// Można dodać tylko jeden punkt.
        /// </remarks>
        /// <param name="komponent"></param>
        /// <exception cref="System.InvalidOperationException">
        /// Jeżeli dodano więcej niż jeden punkt lub punkt jest pusty.
        /// </exception>
        public override void DodajPozycja(PozycjaSwde komponent)
        {
            ZapewnijNotNull(komponent);
            ZapewnijTylkoJedenPunkt();
            _pozycja = komponent;
        }
    }
}
