using swde.Sekcje;
using swde.Rekordy;

namespace swde.Komponenty
{
    /// <summary>
    /// Reprezentuje wskazanie na rekord w pliku SWDE znajdujący się w sekcji obiektów.
    /// </summary>
    internal abstract class ReferencjaSwde : KomponentBase
    {
        protected RekordSwdeBase _rekord;

        /// <summary>
        /// Zwraca powiązany rekord.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Jeżeli powiązany rekord nie istnieje.</exception>
        public RekordSwdeBase Rekord
        {
            get
            {
                GetRekord();
                ZapewnijIstniejeRelacja();
                return _rekord;
            }
        }

        protected SekcjaObiektowSwde _obiekty;

        protected ReferencjaSwde(SekcjaObiektowSwde obiekty)
        {
            _obiekty = obiekty;
        }

        /// <summary>
        /// Zwraca powiązany rekord.
        /// </summary>
        /// <returns>Prawda jeżeli referencja już istnieje, fałsz jeżeli nie.</returns>
        public abstract bool GetRekord();

        protected void ZapewnijIstniejeRelacja()
        {
            KontrolerKontekstu.Zapewnij(_rekord != null, 
                string.Format("Wiązanie do rekordu nie istnieje ({0}).", ToString()));
        }
    }
}
