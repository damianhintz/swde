﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pragmatic.Kontrakty;

namespace egib
{
    //TODO Porównywanie identyfikatorów!
    public class IdentyfikatorDzialki
    {
        private string _numerObrebu;
        private string _numerDzialki;

        public IdentyfikatorDzialki(string numerObrebu, string numerDzialki)
        {
            Kontrakt.requires(!String.IsNullOrEmpty(numerObrebu), "Numer obrębu jest pusty.");
            foreach(var c in numerObrebu) Kontrakt.requires(char.IsDigit(c), "Numer obrębu nie składa się z cyfr:" + numerObrebu);
            Kontrakt.requires(!String.IsNullOrEmpty(numerDzialki), "Numer działki jest pusty.");
            Kontrakt.requires(char.IsDigit(numerDzialki[0]), 
                "Numer działki " + numerDzialki + " nie zaczyna się od cyfry.");
            _numerObrebu = numerObrebu;
            _numerDzialki = numerDzialki;
            Kontrakt.ensures(_numerObrebu.Equals(numerObrebu));
            Kontrakt.ensures(_numerDzialki.Equals(numerDzialki));
        }

        public static IdentyfikatorDzialki parseId(string id)
        {
            //<idobr>-<idd>
            char[] separator = new char[] { '-' };
            int maxFields = 2;
            string[] pola = id.Split(separator, maxFields);
            string numerObrebu = String.Empty;
            string numerDzialki = pola[0];
            if (pola.Length > 1)
            {
                numerObrebu = pola[0];
                numerDzialki = pola[1];
            }
            return new IdentyfikatorDzialki(numerObrebu, numerDzialki);
        }

        public static IdentyfikatorDzialki parseG5(string g5idd)
        {
            //D,G5IDD,D,142302_2.0001.296
            //D,G5IDD,D,142302_2.0007.AR_2.656
            //<teryt>.<obr>.[arkusz].<idd>
            char[] separator = new char[] { '.' };
            string[] pola = g5idd.Split(separator);
            int maxFields = 4;
            int minFields = 3;
            Kontrakt.assert(pola.Length >= minFields && pola.Length <= maxFields);
            string numerTeryt = pola[0];
            string numerObrebu = byte.Parse(pola[1]).ToString();
            string numerDzialki = pola[pola.Length - 1];
            string numerArkusza = string.Empty;
            if (pola.Length == 4) numerArkusza = pola[2].Replace("AR_", "");
            if (!string.IsNullOrEmpty(numerArkusza)) numerDzialki = numerArkusza + "." + numerDzialki;
            return new IdentyfikatorDzialki(numerObrebu, numerDzialki);
        }

        public string identyfikator()
        {
            return string.Format("{0}-{1}", _numerObrebu, _numerDzialki);
        }

        public string numerObrebu()
        {
            return _numerObrebu;
        }

        public string numerDzialki()
        {
            return _numerDzialki;
        }

        public override string ToString()
        {
            return identyfikator();
        }
    }
}
