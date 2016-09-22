using System;
using Pragmatic.Kontrakty;

namespace egib
{
    public class RodzajPunktu
    {
        public static readonly RodzajPunktu Nieznany = new RodzajPunktu(String.Empty, String.Empty);

        private string _g5bpp;
        private string _g5zrd;
        private bool? _pomiar;

        public RodzajPunktu(string g5zrd, string g5bpp)
        {
            _g5bpp = g5bpp;
            _g5zrd = g5zrd;
            _pomiar = jestPomiar();
        }

        public bool nieznany() { return !_pomiar.HasValue; }
        public bool zWektoryzacji() { return _pomiar.HasValue && !_pomiar.Value; }
        public bool zPomiaru() { return _pomiar.HasValue && _pomiar.Value; }

        private bool? jestPomiar()
        {
            bool bpp = false;
            switch(_g5bpp)
            {
                case "1": //0.00 - 0.10
                    bpp = true;
                    break;
                case "2":
                case "3":
                case "4":
                case "5":
                case "6": //powyżej 3
                    bpp = false;
                    break;
                default:
                    return null;
            }
            bool zrd = false;
            switch (_g5zrd)
            {
                case "1": //1* Geodezyjne pomiary terenowe poprzedzone ustaleniem przebiegu granic
                case "5": //5* Zatwierdzone projekty podziału nieruchomości
                    zrd = true;
                    break;
                case "2": //2* Geodezyjne pomiary terenowe nie poprzedzone ustaleniem przebiegu granic
                case "3": //3 Pomiary fotogrametryczne poprzedzone ustaleniem przebiegu granic i ich sygnalizacją
                case "4": //4 Pomiary fotogrametryczne nie poprzedzone ustaleniem przebiegu granic i ich sygnalizacją
                case "6": //6* Scalenia gruntów
                case "7": //7 Digitalizacja mapy lub wektoryzacja automatyczna rastra mapy z jednoczesnym wykorzystaniem wyników geodezyjnych pomiarów terenowych
                case "8": //8 Inne
                case "9": //9* Foto (nowy kod)
                    zrd = false;
                    break;
                default:
                    return null;
            }
            return bpp && zrd;
        }

        public override string ToString()
        {
            string rodzaj = "nieznany";
            if (nieznany()) rodzaj = "nieznany";
            else if (zWektoryzacji()) rodzaj = "z wektoryzacji";
            else if (zPomiaru()) rodzaj = "z operatu";
            else Kontrakt.fail();
            return rodzaj;
        }
    }
}
