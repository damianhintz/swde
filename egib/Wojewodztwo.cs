using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pragmatic.Kontrakty;

namespace egib
{
    public class Wojewodztwo
    {
        public static string nazwa(string teryt)
        {
            string kod = teryt.Substring(0, 2);
            switch (kod)
            {
                case "02": return "DOLNOŚLĄSKIE";
                case "04": return "KUJAWSKO-POMORSKIE";
                case "06": return "LUBELSKIE";
                case "08": return "LUBUSKIE";
                case "10": return "ŁÓDZKIE";
                case "12": return "MAŁOPOLSKIE";
                case "14": return "MAZOWIECKIE";
                case "16": return "OPOLSKIE";
                case "18": return "PODKARPACKIE";
                case "20": return "PODLASKIE";
                case "22": return "POMORSKIE";
                case "24": return "ŚLĄSKIE";
                case "26": return "ŚWIĘTOKRZYSKIE";
                case "28": return "WARMIŃSKO-MAZURSKIE";
                case "30": return "WIELKOPOLSKIE";
                case "32": return "ZACHODNIOPOMORSKIE";
                default: break;
            }
            Kontrakt.fail("Tery zawiera nierozpoznany kod województwa: " + teryt);
            return null;
        }

    }
}
