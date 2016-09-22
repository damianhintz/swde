using egib.swde.Sekcje;

namespace egib.swde
{
    interface IDokumentSwde
    {
        void DodajSekcjaMetadanych(SekcjaMetadanychSwde komponent);
        void DodajSekcjaAtrybutow(SekcjaAtrybutowSwde komponent);
        void DodajSekcjaTypow(SekcjaTypowSwde komponent);
        void DodajSekcjaObiektow(SekcjaObiektowSwde komponent);
    }
}
