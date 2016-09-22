using Pragmatic.Kontrakty;

namespace egib
{
    public abstract class RozliczenieWriter
    {
        private Rozliczenie _rozliczenie;

        protected RozliczenieWriter(Rozliczenie rozliczenie)
        {
            Kontrakt.requires(rozliczenie != null);
            _rozliczenie = rozliczenie;
            Kontrakt.ensures(rozliczenie.Equals(_rozliczenie));
        }

        public Rozliczenie rozliczenie() { return _rozliczenie; }
        public abstract void write(string folderName);
    }
}
