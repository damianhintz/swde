using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using egib.swde.Rekordy;

namespace egib.swde.Sekcje
{
    /// <summary>
    /// Kolekcja rekordów o tym samym identyfikatorze.
    /// </summary>
    class KolekcjaWersji : IEnumerable<RekordSwdeBase>
    {
        private string _id;

        /// <summary>
        /// Identyfikator wersjonowanego obiektu.
        /// </summary>
        public string Id { get { return _id; } }

        private List<RekordSwdeBase> _wersje;
        
        private RekordSwdeBase _aktualna;

        /// <summary>
        /// Aktualna wersja obiektu. Może być tylko jedna aktualna wersja.
        /// </summary>
        public RekordSwdeBase Aktualna { get { return _aktualna; } }

        public KolekcjaWersji(string id)
        {
            _id = id;
            _wersje = new List<RekordSwdeBase>();
        }

        /// <summary>
        /// Może być tylko jedna aktualna wersja obiektu.
        /// </summary>
        private bool ZapewnijTylkoJednaAktualnaWersja()
        {
            //KontrolerKontekstu.Zapewnij(_aktualna == null, string.Format(
            //    "Może być tylko jedna aktualna wersja obiektu SWDE, ID={0}.", _id));
            return _aktualna == null;
        }

        public bool DodajWersje(RekordSwdeBase rekord)
        {
            //Identyfikator obiektu powinien być zgodny.
            if (_id != rekord.Id) throw new InvalidOperationException("Identyfikator obiektu jest niezgodny.");
            if (rekord.Wersja == WersjaObiektu.Aktualna)
            {
                if (!ZapewnijTylkoJednaAktualnaWersja())
                {
                    LoggerSwde.PowtorzonaAktualnaWersjaObiektu(rekord);
                    return false;
                }
                _aktualna = rekord;
            }
            _wersje.Add(rekord);
            return true;
        }

        public IEnumerator<RekordSwdeBase> GetEnumerator()
        {
            return _wersje.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
