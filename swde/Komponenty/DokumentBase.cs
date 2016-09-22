using System.Collections.Generic;

using egib.swde.Sekcje;

namespace egib.swde.Komponenty
{
    internal abstract class DokumentBase : KomponentBase
    {
        protected SekcjaMetadanychSwde _metadane;

        /// <summary>
        /// Sekcja metadanych pliku SWDE.
        /// </summary>
        public SekcjaMetadanychSwde Metadane { get { return _metadane; } }

        protected SekcjaAtrybutowSwde _atrybuty;

        protected SekcjaTypowSwde _typy;

        protected SekcjaObiektowSwde _obiekty;

        /// <summary>
        /// Sekcja obiektów pliku SWDE.
        /// </summary>
        public SekcjaObiektowSwde Obiekty { get { return _obiekty; } }

        public DokumentBase()
        {
        }

        private void ZapewnijNotNull(SekcjaSwdeBase komponent)
        {
            KontrolerKontekstu.Zapewnij(komponent != null, "Nie można dodać pustej sekcji do dokumentu.");
        }

        private void ZapewnijTylkoJednaSekcjaDanegoTypu(SekcjaSwdeBase sekcja, SekcjaSwdeBase aktualnaSekcja)
        {
            KontrolerKontekstu.Zapewnij(aktualnaSekcja == null,
                string.Format("Można dodać tylko jedną sekcję typu {0}.", sekcja));
        }

        public override void DodajSekcjaMetadanych(SekcjaMetadanychSwde komponent)
        {
            ZapewnijNotNull(komponent);
            ZapewnijTylkoJednaSekcjaDanegoTypu(komponent, _metadane);
            _metadane = komponent;
        }

        public override void DodajSekcjaAtrybutow(SekcjaAtrybutowSwde komponent)
        {
            ZapewnijNotNull(komponent);
            ZapewnijTylkoJednaSekcjaDanegoTypu(komponent, _atrybuty);
            _atrybuty = komponent;
        }

        public override void DodajSekcjaTypow(SekcjaTypowSwde komponent)
        {
            ZapewnijNotNull(komponent);
            ZapewnijTylkoJednaSekcjaDanegoTypu(komponent, _typy);
            _typy = komponent;
        }

        public override void DodajSekcjaObiektow(SekcjaObiektowSwde komponent)
        {
            ZapewnijNotNull(komponent);
            ZapewnijTylkoJednaSekcjaDanegoTypu(komponent, _obiekty);
            _obiekty = komponent;
        }
    }
}
