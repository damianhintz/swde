using System.IO;
using swde.Konstruktor;
using swde.Komponenty;
using Pragmatic.Kontrakty;

namespace swde
{
    internal class DokumentReader : DokumentBase
    {
        private BudowniczySwde _budowniczy;
        private SwdeReader _reader;

        public DokumentReader(string fileName)
        {
            Kontrakt.requires(File.Exists(fileName), "Plik SWDE nie istnieje: " + fileName);
            _budowniczy = new BudowniczySwde(this);
            _reader = new SwdeReader(_budowniczy);
            _reader.Read(fileName);
            Kontrakt.ensures(_budowniczy.CzyZbudowany, "Plik SWDE nie został prawidłowo wczytany.");
        }

        //obiektyAktualne
        //obiektyKlasy
        //obiekty
        //osobyFizyczne
        //obreby
    }
}
