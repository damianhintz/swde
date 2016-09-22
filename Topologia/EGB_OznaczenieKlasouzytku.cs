using System;
using System.Collections.Generic;

namespace egib.TopologiaKonturuów
{
    public class EGB_OznaczenieKlasouzytku
    {
        public EGB_OFU OFU;
        public EGB_OZU? OZU;
        public EGB_OZK? OZK;

        public EGB_OznaczenieKlasouzytku(EGB_OFU ofu, EGB_OZU? ozu, EGB_OZK? ozk)
        {
            this.OFU = ofu;
            this.OZU = ozu;
            this.OZK = ozk;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", OFU, OZU, OZK);
        }

        public void walidujOgraniczenia()
        {
            if (!wystepowanieOZUiOFU()) Logger.writeWarning("Atrybuty OZU i OZK muszą występować łącznie: " + this.ToString());
            if (!zaleznoscOFUiOZUiOZKdlaR()) Logger.writeWarning("Dla OZU = R, OZK przyjmuje jedną z następujących wartości: I, II, IIIa, IIIb, IVa, IVb, V, VI, VIz: " + this.ToString());
            if (!zaleznoscOFUiOZUiOZKnieR()) Logger.writeWarning("Dla OZU = Ł, Ps, Ls, Lz, OZK przyjmuje jedną z następujących wartości: I, II, III, IV, V, VI: " + this.ToString());
            //if (!tworzenieOznaczenia1()) Logger.writeWarning("Oznaczenie klasoużytku przyjmuje wartość OFU w przypadku użytków grunowych nie objętych gleboznawczą klasyfikacją gruntów, tj. dla OFU = Lz, B, Ba, Bi, Bp, Bz, K, dr, Tk, Ti, Tp, E-Lz, E-Wp, E-Ws, E-N, N, Wm, Wp, Ws, Tr, Ls oraz E-Ls: " + this.ToString());
        }

        private bool wystepowanieOZUiOFU()
        {
            //Atrybuty OZU i OZK muszą występować łącznie.
            //self.OZU-->NotEmpty implies self.OZK-->NotEmpty
            return (OZU.HasValue && OZK.HasValue) || (!OZU.HasValue && !OZK.HasValue);
        }

        private bool zaleznoscOFUiOZUiOZKdlaR()
        {
            if (!OZU.HasValue) return true;
            if (!OZK.HasValue) return true;
            EGB_OZU ozu = OZU.Value;
            EGB_OZK ozk = OZK.Value;
            //Przyjęcie przez OFU wartości: 
            //('R' lub 'S' lub 'Br' lub 'Wsr' lub 'W' lub 'Lzr' lub 'E?' lub 'E-Lz' lub 'E-W') 
            //i przez OZU wartości 'R' powoduje, 
            //że OZK może przyjąć jedną z wartości ('I' lub 'II' lub 'IIIa' lub 'IIIb' lub 'IVa' lub 'IVb' lub 'V' lub 'VI' lub 'VIz').
            if (ofuDlaR(OFU))
            {
                if (ozu.Equals("R"))
                {
                    if (!ozkDlaR(ozk)) return false;
                }
            }
            return true;
        }

        private bool ofuDlaR(EGB_OFU ofu)
        {
            return ofu.Equals("R") || ofu.Equals("S") || ofu.Equals("Br") || ofu.Equals("Wsr") || ofu.Equals("W") ||
                ofu.Equals("Lzr") || ofu.Equals("E") || ofu.Equals("E-Lz") || ofu.Equals("E-W");
        }

        private bool ozkDlaR(EGB_OZK ozk)
        {
            return ozk.Equals("I") || ozk.Equals("II") || ozk.Equals("IIIa") || ozk.Equals("IIIb") ||
                ozk.Equals("IVa") || ozk.Equals("IVb") || ozk.Equals("V") || ozk.Equals("VI") || ozk.Equals("VIz");
        }

        public bool zaleznoscOFUiOZUiOZKnieR()
        {
            if (!OZU.HasValue) return true;
            if (!OZK.HasValue) return true;
            EGB_OZU ozu = OZU.Value;
            EGB_OZK ozk = OZK.Value;
            //Przyjęcie przez OFU wartości: 
            //('Ł' lub 'Ps' lub 'S' lub 'Br' lub 'Wsr' lub 'W' lub 'Lzr' lub 'E' lub 'E-Lz' lub 'E-W') 
            //i przez OZU wartości: ('Ł' lub 'Ps') lub przyjęcie przez OFU wartości ('Ls' lub 'E-Ls') 
            //i przez OZU wartości 'Ls' lub przyjęcie przez OFU wartości ('Lz' lub 'E-Lz') 
            //i przez OZU wartości 'Lz' powoduje, 
            //że OZK może przyjąć jedną z wartości ('I' lub 'II' lub 'III' lub 'IV' lub 'V' lub 'VI').

            /*inv: 
             * if (OFU='Ł' or OFU='Ps' or OFU='S' or OFU='Br' or OFU='Wsr' or OFU='W' or OFU='Lzr' or OFU='E?' or OFU='E-Lz' or OFU='E-W') 
             * and (OZU='Ł' or OZU='Ps') or ((OFU='Ls' or OFU='E-Ls') and (OZU='Ls')) or ((OFU='Lz' or OFU='E-Lz') and (OZU='Lz'))
             * implies (OZK='I' or OZK='II' or OZK='III' or OZK='IV' or OZK='V' or OZK='VI')*/
            if (ofuNieR(OFU))
            {
                if (ozu.Equals("Ł") || ozu.Equals("Ps")) if (!ozkNieR(ozk)) return false;
                if (ozu.Equals("Ls") && (OFU.Equals("Ls") || OFU.Equals("E-Ls"))) if (!ozkNieR(ozk)) return false;
                if (ozu.Equals("Lz") && (OFU.Equals("Lz") || OFU.Equals("E-Lz"))) if (!ozkNieR(ozk)) return false;
            }
            return true;
        }

        private bool ofuNieR(EGB_OFU ofu)
        {
            return ofu.Equals("Ł") || ofu.Equals("Ps") || ofu.Equals("S") || ofu.Equals("Br") || ofu.Equals("Wsr") ||
                ofu.Equals("W") || ofu.Equals("Lzr") || ofu.Equals("E") || ofu.Equals("E-Lz") || ofu.Equals("E-W");
        }

        private bool ozkNieR(EGB_OZK ozk)
        {
            return ozk.Equals("I") || ozk.Equals("II") || ozk.Equals("III") ||
                ozk.Equals("IV") || ozk.Equals("V") || ozk.Equals("VI");
        }

        private bool tworzenieOznaczenia1()
        {
            /*1. Oznaczenie klasoużytku przyjmuje wartość OFU w przypadku użytków grunowych nie objętych gleboznawczą klasyfikacją gruntów, 
             * tj. dla OFU = Lz, B, Ba, Bi, Bp, Bz, K, dr, Tk, Ti, Tp, E-Lz, E-Wp, E-Ws, E-N, N, Wm, Wp, Ws, Tr, Ls oraz E-Ls.*/
            HashSet<string> set = new HashSet<string>("Lz,B,Ba,Bi,Bp,Bz,K,dr,Tk,Ti,Tp,E-Lz,E-Wp,E-Ws,E-N,N,Wm,Wp,Ws,Tr,Ls,E-Ls".Split(','));
            string ofu = OFU.ofu;
            if (set.Contains(ofu)) return !OZU.HasValue && !OZK.HasValue;
            return true;
        }
    }
}
