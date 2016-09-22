using egib.swde.Rekordy;

namespace egib.swde.Komponenty
{
    interface ISekcjaMetadanychSwde
    {
        void DodajMetadane(MetadaneSwde komponent);
    }

    interface ISekcjaTypowSwde
    {
        /// <summary>
        /// Budowa definicji typu.
        /// </summary>
        /// <param name="komponent"></param>
        void DodajDefinicjaTypu(DefinicjaTypuSwde komponent);
    }

    interface ISekcjaObiektowSwde
    {
        void DodajRekordOpisowy(RekordOpisowySwde komponent);
        void DodajRekordZlozony(RekordZlozonySwde komponent);
        void DodajRekordPunktowy(RekordPunktowySwde komponent);
        void DodajRekordLiniowy(RekordLiniowySwde komponent);
        void DodajRekordObszarowy(RekordObszarowySwde komponent);
        //void DodajRekordModelutTerenu(RekordModeluTerenu komponent);
    }

    interface IRekordNieprzestrzennySwde
    {
        /// <summary>
        /// Dodanie atrybutu do rekordu.
        /// </summary>
        void DodajAtrybut(AtrybutSwde komponent);

        /// <summary>
        /// Dodanie wiązania do rekordu.
        /// </summary>
        void DodajWiazanie(WiazanieSwdeBase komponent);
    }

    interface IObszarSwde
    {
        void DodajKonturZew();
        void DodajKonturWew();
    }

    interface IRekordPrzestrzennySwde : IObszarSwde
    {
        /// <summary>
        /// Dodanie pozycji do rekordu obszaru, linii lub punktu.
        /// </summary>
        void DodajPozycja(PozycjaSwde komponent);

        /// <summary>
        /// Budowa linii lub obszaru.
        /// </summary>
        RekordLiniaSwde DodajLiniaLubObszar();

        /// <summary>
        /// Dodanie ostatniej pozycji do rekordu obszaru lub linii.
        /// </summary>
        void DodajWezelKoncowy();
    }
}
