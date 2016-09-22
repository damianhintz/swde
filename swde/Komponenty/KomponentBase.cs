using System;
using System.Collections.Generic;
using swde.Sekcje;
using swde.Rekordy;

namespace swde.Komponenty
{
    /// <summary>
    /// Abstrakcyjny komponent pliku SWDE.
    /// </summary>
    internal abstract class KomponentBase :
        IDokumentSwde, 
        ISekcjaMetadanychSwde, ISekcjaTypowSwde, ISekcjaObiektowSwde,
        IRekordNieprzestrzennySwde,
        IRekordPrzestrzennySwde
    {
        private bool _closed;

        protected KomponentBase()
        {
            Begin();
        }

        /// <summary>
        /// Początek konstrukcji komponentu.
        /// </summary>
        public void Begin()
        {
            _closed = false;
        }

        /// <summary>
        /// Koniec konstrukcji komponentu.
        /// </summary>
        /// <param name="terminator"></param>
        public void End(KomponentBase terminator)
        {
            KontrolerKontekstu.Zapewnij(terminator.JestTerminatorem(this), 
                string.Format("{0} nie jest terminatorem {1}", terminator, this));

            if (_closed)
                throw new InvalidOperationException(
                    string.Format("Komponent {0} został już domknięty.", this));

            _closed = true;
        }

        /// <summary>
        /// Czy ten komponent jest terminatorem danego komponentu.
        /// </summary>
        /// <param name="komponent"></param>
        /// <returns></returns>
        public virtual bool JestTerminatorem(KomponentBase komponent)
        {
            throw new NotImplementedException(
                string.Format("{0} nie jest terminatorem komponentu {1}.", this, komponent));
        }

        private string GetPrzyczynaDla(Type item)
        {
            return string.Format("Nie można dodać {0} do {1}.", item, this);
        }

        #region Interfejs dokumentu

        public virtual void DodajSekcjaMetadanych(SekcjaMetadanychSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajSekcjaAtrybutow(SekcjaAtrybutowSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajSekcjaTypow(SekcjaTypowSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajSekcjaObiektow(SekcjaObiektowSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        #endregion

        #region Interfejs sekcji metadanych

        public virtual void DodajMetadane(MetadaneSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        #endregion

        #region Interfejs sekcji typów

        public virtual void DodajDefinicjaTypu(DefinicjaTypuSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        #endregion

        #region Interfejs sekcji obiektów

        public virtual void DodajRekordOpisowy(RekordOpisowySwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }
        
        public virtual void DodajRekordZlozony(RekordZlozonySwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajRekordPunktowy(RekordPunktowySwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajRekordLiniowy(RekordLiniowySwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajRekordObszarowy(RekordObszarowySwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        #endregion

        #region Interfejs rekordu nieprzestrzennego

        public virtual void DodajAtrybut(AtrybutSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual void DodajWiazanie(WiazanieSwdeBase komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        #endregion

        #region Interfejs rekordu przestrzennego

        public virtual void DodajPozycja(PozycjaSwde komponent)
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(komponent.GetType()));
        }

        public virtual RekordLiniaSwde DodajLiniaLubObszar()
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(typeof(RekordLiniaSwde)));
            return null;
        }

        public virtual void DodajWezelKoncowy()
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(typeof(WezelKoncowySwde)));
        }

        public virtual void DodajKonturZew()
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(typeof(KonturZewnetrzny)));
        }

        public virtual void DodajKonturWew()
        {
            KontrolerKontekstu.Zapewnij(false, GetPrzyczynaDla(typeof(KonturWewnetrzny)));
        }

        #endregion

    }
}
