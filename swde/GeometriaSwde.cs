using System;
using System.Collections.Generic;

using egib.swde.Rekordy;
using egib.swde.Komponenty;
using egib.swde.Geometria;

namespace egib.swde
{
    /// <summary>
    /// Reprezentuje geometrię rekordu przestrzennego w pliku SWDE.
    /// </summary>
    public abstract class GeometriaSwde
    {
        private ObiektSwde[] _punktyGraniczne;
        private int _punktyNiegraniczne;

        public ObiektSwde[] PunktyGraniczne { get { return _punktyGraniczne; } }
        public int countPunktyGraniczne() { return _punktyGraniczne.Length; }
        public int countPunktyNiegraniczne() { return _punktyNiegraniczne; }

        /// <summary>
        /// Zamienia geometrię na reprezentację w postaci WKT.
        /// Domyślnie współrzędne są odwracane.
        /// </summary>
        /// <param name="trzeciWymiar">czy dodać trzeci wymiar (brak implementacji)</param>
        /// <returns></returns>
        public abstract string NaWkt(bool trzeciWymiar = false);

        public virtual string NaPunktWkt()
        {
            throw new ApplicationException("Tej geometrii nie można zamienić na punkt.");
        }

        public virtual string NaObszarWkt()
        {
            throw new ApplicationException("Tej geometrii nie można zamienić na obszar.");
        }

        public virtual string NaMultiObszarWkt()
        {
            throw new ApplicationException("Tej geometrii nie można zamienić na multiobszar.");
        }

        /// <summary>
        /// Tworzy geometrię odpowiadającą rekordowi przestrzennemu, zwraca null jeżeli rekord jest nieprzestrzenny.
        /// </summary>
        internal static GeometriaSwde createGeometryOrNull(DokumentSwde dokument, RekordSwdeBase rekord)
        {
            GeometriaSwde geometria = null;

            if (rekord is RekordPunktowySwde)
            {
                RekordPunktowySwde rekordPunktowy = rekord as RekordPunktowySwde;
                geometria = new PunktSwde(rekordPunktowy, dokument.Geodezyjny);
                geometria._punktyGraniczne = GetPunktyGraniczne(dokument, rekordPunktowy, out geometria._punktyNiegraniczne);
            }
            else if (rekord is RekordLiniowySwde) //Liniowym elementem jest tylko granica.
            {
                RekordLiniowySwde rekordLiniowy = rekord as RekordLiniowySwde;
                geometria = new MultiLiniaSwde(rekordLiniowy, dokument.Geodezyjny);
                geometria._punktyGraniczne = GetPunktyGraniczne(dokument, rekordLiniowy, out geometria._punktyNiegraniczne);
            }
            else if (rekord is RekordObszarowySwde)
            {
                RekordObszarowySwde rekordObszarowy = rekord as RekordObszarowySwde;
                geometria = new MultiObszarSwde(rekordObszarowy, dokument.Geodezyjny);
                geometria._punktyGraniczne = GetPunktyGraniczne(dokument, rekordObszarowy, out geometria._punktyNiegraniczne);
            }

            return geometria;
        }

        private static ObiektSwde[] GetPunktyGraniczne(DokumentSwde dokument, RekordPunktowySwde rekordPunktowy, out int punktyNiegraniczne)
        {
            punktyNiegraniczne = 0;
            return new ObiektSwde[0];
        }

        private static ObiektSwde[] GetPunktyGraniczne(DokumentSwde dokument, RekordLiniowySwde rekordLiniowy, out int punktyNiegraniczne)
        {
            List<ObiektSwde> punktyGraniczne = new List<ObiektSwde>();
            HashSet<RekordId> rekordyDodane = new HashSet<RekordId>();
            punktyNiegraniczne = 0;
            foreach (var linia in rekordLiniowy.Linie)
            {
                foreach (var pozycja in linia.Segmenty)
                {
                    if (pozycja is PozycjaIdSwde || pozycja is PozycjaIdrSwde)
                    {
                        RekordSwdeBase rekord = pozycja.Rekord;

                        if (rekordyDodane.Add(rekord.Identyfikator))
                        {
                            punktyGraniczne.Add(dokument.createObiekt(rekord));
                        }
                    }
                    else
                    {
                        punktyNiegraniczne++;
                    }
                }
            }
            return punktyGraniczne.ToArray();
        }

        private static ObiektSwde[] GetPunktyGraniczne(DokumentSwde dokument, RekordObszarowySwde rekordObszarowy, out int punktyNiegraniczne)
        {
            List<ObiektSwde> punktyGraniczne = new List<ObiektSwde>();
            HashSet<RekordId> rekordyDodane = new HashSet<RekordId>();
            punktyNiegraniczne = 0;
            foreach (var obszar in rekordObszarowy.Obszary)
            {
                foreach (var pozycja in obszar.Segmenty)
                {
                    if (pozycja is PozycjaIdSwde || pozycja is PozycjaIdrSwde)
                    {
                        RekordSwdeBase rekord = pozycja.Rekord;

                        if (rekordyDodane.Add(rekord.Identyfikator))
                        {
                            punktyGraniczne.Add(dokument.createObiekt(rekord));
                        }
                    }
                    else
                    {
                        punktyNiegraniczne++;
                    }
                }
            }
            return punktyGraniczne.ToArray();
        }
    }
}
