using System;
using System.Collections;
using System.Collections.Generic;

using egib.swde.Rekordy;

namespace egib.swde.Sekcje
{
    /// <summary>
    /// Sekcja obiektów.
    /// </summary>
    internal class SekcjaObiektowSwde : SekcjaSwdeBase, IEnumerable<RekordSwdeBase>
    {
        private HashSet<RekordId> _identyfikatory;
        protected List<RekordSwdeBase> _rekordy;

        /// <summary>
        /// Indeks po identyfikatorze rekordu (powinien być unikatowy w ramach sekcji).
        /// </summary>
        protected IndeksRekordow _indeksRekordow;

        /// <summary>
        /// Indeks po typie obiektu oraz identyfikatorze. Typ obiektu odpowiada jednej tabeli w bazie danych.
        /// Pozwala na wyszukiwanie wersji obiektu.
        /// </summary>
        protected IndeksTabel _indeksTabel;

        public SekcjaObiektowSwde()
        {
            _identyfikatory = new HashSet<RekordId>();
            RekordId.Reset();
            _rekordy = new List<RekordSwdeBase>();
            _indeksRekordow = new IndeksRekordow();
            _indeksTabel = new IndeksTabel();
        }

        public int Count { get { return _rekordy.Count; } }

        public IEnumerator<RekordSwdeBase> GetEnumerator()
        {
            return _rekordy.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override void DodajRekordOpisowy(RekordOpisowySwde komponent)
        {
            DodajRekord(komponent);
        }

        public override void DodajRekordZlozony(RekordZlozonySwde komponent)
        {
            DodajRekord(komponent);
        }

        public override void DodajRekordPunktowy(RekordPunktowySwde komponent)
        {
            DodajRekord(komponent);
        }

        public override void DodajRekordLiniowy(RekordLiniowySwde komponent)
        {
            DodajRekord(komponent);
        }

        public override void DodajRekordObszarowy(RekordObszarowySwde komponent)
        {
            DodajRekord(komponent);
        }

        /// <summary>
        /// Dodaj rekord so sekcji.
        /// </summary>
        /// <param name="rekord"></param>
        /// <exception cref="ArgumentException">
        /// Jeżeli, unikatowy identyfikator dla rekordu został powtórzony w ramach sekcji.
        /// </exception>
        private void DodajRekord(RekordSwdeBase rekord)
        {
            KontrolerKontekstu.Zapewnij(_identyfikatory.Add(rekord.Identyfikator), 
                "Nie można dodać rekordu z tym samym identyfikatorem.");
            
            //Pomiń puste Idr.
            if (_indeksRekordow.Dodaj(rekord))
            {
                //Pomiń puste Id.
                if (_indeksTabel.DodajRekord(rekord))
                {
                    _rekordy.Add(rekord);
                }
            }
        }

        /// <summary>
        /// Szukaj rekordu o podanym identyfikatorze.
        /// </summary>
        /// <param name="idr">Identyfikator rekordu.</param>
        /// <returns>Znaleziony rekord lub null jeżeli brak.</returns>
        public RekordSwdeBase SzukajRekordu(string idr)
        {
            return _indeksRekordow.Szukaj(idr);
        }

        /// <summary>
        /// Szukaj aktualnej wersji danego obiektu.
        /// </summary>
        /// <param name="typ">Kod klasy.</param>
        /// <param name="id">Identyfikator obiektu.</param>
        /// <returns>Znaleziony rekord lub null jeżeli brak.</returns>
        public RekordSwdeBase SzukajObiektu(string typ, string id)
        {
            TabelaSwde tabela = _indeksTabel.Szukaj(typ);
            if (tabela == null) return null;
            return tabela.SzukajObiektu(id);
        }

        /// <summary>
        /// Szukaj wszystkich wersji danego obiektu.
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KolekcjaWersji SzukajWersji(string typ, string id)
        {
            TabelaSwde tabela = _indeksTabel.Szukaj(typ);
            if (tabela == null) return null;
            return tabela.SzukajWersji(id);
        }

        /// <summary>
        /// Zwróć tabelę obiektów danego typu.
        /// </summary>
        /// <param name="typ"></param>
        /// <returns>null, jeżeli brak.</returns>
        public TabelaSwde SzukajTabeli(string typ)
        {
            return _indeksTabel.Szukaj(typ);
        }
    }
}
