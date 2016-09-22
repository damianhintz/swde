using System;
using System.Collections.Generic;
using System.IO;
using swde.Rekordy;

namespace swde
{
    public static class LoggerSwde
    {
        private static List<string> _wpisy = new List<string>();

        public static int Bledy { get; private set; }

        public static void Open(bool append = false)
        {
            if (!append || _wpisy == null)
            {
                _wpisy = new List<string>();
                Bledy = 0;
            }
        }

        public static void Close()
        {
            _wpisy = null;
            Bledy = 0;
        }

        public static void savaAs(string sciezkaZapisu)
        {
            using (StreamWriter writer = new StreamWriter(@sciezkaZapisu, false))
            {
                foreach (var komunikat in _wpisy)
                {
                    writer.WriteLine(komunikat);
                }
            }
        }

        public static void save()
        {
            foreach (var komunikat in _wpisy)
            {
                Console.WriteLine(komunikat);
            }
        }

        public static void error(string komunikat)
        {
            Bledy++;
            _wpisy.Add(string.Format("{0} <Błąd>: {1}", DateTime.Now, komunikat));
        }

        internal static void PowtorzonyIdentyfikatorRekordu(RekordSwdeBase rekord)
        {
            error(string.Format("{0} <ID:{1},IDR:{2}>: Powtórzony identyfikator rekordu SWDE (IDR). Rekord został pominięty.", rekord.Typ, rekord.Id, rekord.Idr));
        }

        internal static void PowtorzonaAktualnaWersjaObiektu(RekordSwdeBase rekord)
        {
            error(string.Format("{0} <ID:{1},IDR:{2}>: Powtórzona aktualna wersja obiektu SWDE (ID). Rekord został pominięty.", rekord.Typ, rekord.Id, rekord.Idr));
        }
    }
}
