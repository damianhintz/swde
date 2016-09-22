using System.Collections.Generic;

using swde.Rekordy;

namespace swde.Sekcje
{
    /// <summary>
    /// Indeks tabel dostępnych po nazwie typu.
    /// </summary>
    internal class IndeksTabel
    {
        private Dictionary<string, TabelaSwde> _tabele;

        public IndeksTabel()
        {
            _tabele = new Dictionary<string, TabelaSwde>();
        }

        /// <summary>
        /// Dodaj nowy rekord do tabeli. Indeks tabel jest budowany automatycznie na podstawie typu rekordu.
        /// </summary>
        /// <param name="rekord"></param>
        public bool DodajRekord(RekordSwdeBase rekord)
        {
            string typ = RekordSwdeG5.NormalizujPrefiksTypu(rekord.Typ);

            if (!_tabele.ContainsKey(typ))
            {
                _tabele.Add(typ, new TabelaSwde(typ));
            }

            //Mogą być różne wersje obiektu o tym samym id.
            TabelaSwde tabelaTypu = _tabele[typ];
            return tabelaTypu.DodajObiekt(rekord);
        }

        /// <summary>
        /// Szuka tabeli o podanej nazwie.
        /// </summary>
        /// <param name="typ">Nazwa tabeli/klasy.</param>
        /// <returns>Znaleziona tabela lub null, jeżeli brak.</returns>
        public TabelaSwde Szukaj(string typ)
        {
            if (string.IsNullOrEmpty(typ)) return null;
            typ = RekordSwdeG5.NormalizujPrefiksTypu(typ);
            if (!_tabele.ContainsKey(typ)) return null;
            return _tabele[typ];
        }
    }
}
