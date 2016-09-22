using Pragmatic.Kontrakty;

namespace egib
{
    public abstract class ObiektPowierzchniowy
    {
        private Powierzchnia _powierzchnia;
        
        protected ObiektPowierzchniowy(Powierzchnia powierzchnia)
        {
            Kontrakt.requires(powierzchnia != null);
            _powierzchnia = powierzchnia;
        }

        public Powierzchnia powierzchnia()
        {
            return _powierzchnia;
        }

        public void powierzchnia(Powierzchnia powierzchnia)
        {
            _powierzchnia = powierzchnia;
        }

    }
}
