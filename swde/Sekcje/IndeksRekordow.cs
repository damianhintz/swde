using System.Collections.Generic;

using swde.Rekordy;

namespace swde.Sekcje
{
    /// <summary>
    /// Indeks rekordów dostępnych po identyfikatorze IDR.
    /// </summary>
    internal class IndeksRekordow
    {
        private Dictionary<string, RekordSwdeBase> _rekordy;

        public IndeksRekordow()
        {
            _rekordy = new Dictionary<string, RekordSwdeBase>();
        }

        private bool ZapewnijUnikatowyIdr(RekordSwdeBase rekord)
        {
            string idr = rekord.Idr;
            bool powtorzonyIdr = _rekordy.ContainsKey(idr);
            //KontrolerKontekstu.Zapewnij(!powtorzonyIdr, string.Format("Identyfikator rekordu musi być unikatowy <{0}>.", idr));
            if (powtorzonyIdr) LoggerSwde.PowtorzonyIdentyfikatorRekordu(rekord);
            return !powtorzonyIdr;
        }

        /// <summary>
        /// Dodaj nowy rekord do indeksu. Może być tylko jeden rekord o podanym idr.
        /// Rekordy z pustym idr nie są dodawane do indeksu.
        /// </summary>
        /// <remarks>
        /// W pliku SWDE mogą pojawić się rekordy o tym samym idr, ten typ błędu jest niedopuszczalny.
        /// </remarks>
        /// <param name="rekord"></param>
        /// <exception cref="System.InvalidOperationException">Jeżeli identyfikator zostanie powtórzony.</exception>
        public bool Dodaj(RekordSwdeBase rekord)
        {
            string idr = rekord.Idr;
            if (string.IsNullOrEmpty(rekord.Idr)) return true;
            if (!ZapewnijUnikatowyIdr(rekord)) return false;
            _rekordy.Add(rekord.Idr, rekord);
            return true;
        }

        /// <summary>
        /// Szuka rekordu o podanym idr.
        /// </summary>
        /// <param name="idr">Identyfikator rekordu.</param>
        /// <returns>Znaleziony rekord lub null, jeżeli brak rekordu.</returns>
        public RekordSwdeBase Szukaj(string idr)
        {
            if (!_rekordy.ContainsKey(idr)) return null;

            return _rekordy[idr];
        }
    }
}
