using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Pragmatic.Kontrakty;

namespace egib
{
    public class EwmapaRozliczenieReader : RozliczenieReader
    {
        FabrykaKlasouzytku _klu;
        RodzajDzialki _rodzajDzialki;

        public EwmapaRozliczenieReader(RodzajDzialki rodzajDzialki)
        {
            _rodzajDzialki = rodzajDzialki;
        }

        public override Rozliczenie read(string fileName)
        {
            _klu = new FabrykaKlasouzytku();
            _klu.read(Path.Combine(Path.GetDirectoryName(fileName), "uzytkiG5.csv"));

            Kontrakt.requires(File.Exists(fileName));
            string[] lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            string headerLine = lines[0];
            Kontrakt.assert(
                headerLine.Equals("Rozliczenie konturów klasyfikacyjnych na działkach [m^2]"),
                "Nieprawidłowy nagłówek pliku z rozliczeniem konturów.");
            Rozliczenie rozliczenie = new Rozliczenie(_rodzajDzialki, _klu);
            int wczytaneDzialki = 0;
            List<string> rekordDzialki = new List<string>();
            for (int i = 1; i < lines.Length; i++)
            {
                string linia = lines[i].Trim();
                if (koniecRekorduDzialki(rekordDzialki, linia))
                {
                    readRekordDzialki(rozliczenie, rekordDzialki);
                    wczytaneDzialki++;
                }
                else
                {
                    //Kumulacja rekordu działki wraz z użytkami.
                    if (!String.IsNullOrEmpty(linia)) rekordDzialki.Add(linia);
                }
            }
            Kontrakt.ensures(rozliczenie != null);
            Kontrakt.ensures(rekordDzialki.Count == 0, "Nieprawidłowo zakończony rekord działki");
            Kontrakt.ensures(rozliczenie.Count() > 0);
            Kontrakt.ensures(wczytaneDzialki == rozliczenie.Count());
            return rozliczenie;
        }

        private void readRekordDzialki(Rozliczenie rozliczenie, List<string> rekordDzialki)
        {
            //1-1                      19857
            string liniaDzialki = rekordDzialki[0];
            DzialkaEwidencyjna dzialka = parseDzialka(liniaDzialki);
            //Pomijamy pierwszy rekord reprezentujący działkę.
            for (int i = 1; i < rekordDzialki.Count; i++)
            {
                //  LsVI                    3987
                string liniaKlasouzytku = rekordDzialki[i];
                Klasouzytek klasouzytek = parseKlasouzytek(liniaKlasouzytku);
                dzialka.dodajKlasouzytek(klasouzytek);
            }
            rozliczenie.dodajDzialka(dzialka);
            //Rozpoczęcie kumulacji nowego rekordu.
            rekordDzialki.Clear();
        }

        private bool koniecRekorduDzialki(List<string> rekord, string linia)
        {
            return string.IsNullOrEmpty(linia) && rekord.Count > 0;
        }

        private DzialkaEwidencyjna parseDzialka(string linia)
        {
            string[] pola = linia.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            IdentyfikatorDzialki identyfikator = IdentyfikatorDzialki.parseId(pola[0]);
            Powierzchnia powierzchnia = Powierzchnia.parseMetry(pola[1]);
            DzialkaEwidencyjna dzialka = new DzialkaEwidencyjna(identyfikator, powierzchnia);
            return dzialka;
        }

        private Klasouzytek parseKlasouzytek(string linia)
        {
            string[] pola = linia.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string oznaczenie = pola[0];
            Powierzchnia powierzchnia = Powierzchnia.parseMetry(pola[1]);
            string[] klu = _klu.map(oznaczenie);
            Klasouzytek klasouzytek = new Klasouzytek(klu[0], klu[1], klu[2], powierzchnia);
            string ozn = klasouzytek.oznaczenie(_klu);
            Kontrakt.ensures(oznaczenie.Equals(ozn), "Odtworzenie oznaczenia nie jest możliwe: " + oznaczenie + " z " + 
                klasouzytek.ToString() + " = " + ozn);
            return klasouzytek;
        }
    }
}
