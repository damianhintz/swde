using swde.Sekcje;

namespace swde
{
    interface IDokumentSwde
    {
        void DodajSekcjaMetadanych(SekcjaMetadanychSwde komponent);
        void DodajSekcjaAtrybutow(SekcjaAtrybutowSwde komponent);
        void DodajSekcjaTypow(SekcjaTypowSwde komponent);
        void DodajSekcjaObiektow(SekcjaObiektowSwde komponent);
    }
}
