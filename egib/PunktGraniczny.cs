using Pragmatic.Kontrakty;

namespace egib
{
    public class PunktGraniczny
    {
        private string _bladPolozenia;
        private string _zrodloDanych;
        private RodzajPunktu _rodzaj;
        private string _operat = string.Empty; //Sygnatura dokumentu.

        public PunktGraniczny(string zrodloDanych, string bladPolozenia)
        {
            _bladPolozenia = bladPolozenia;
            _zrodloDanych = zrodloDanych;
            _rodzaj = new RodzajPunktu(zrodloDanych, bladPolozenia);
            Kontrakt.ensures(_zrodloDanych.Equals(zrodloDanych));
            Kontrakt.ensures(_rodzaj != null);
            Kontrakt.ensures(_operat.Length == 0);
        }

        public string bladPolozenia() { return _bladPolozenia; }
        public string zrodloDanych() { return _zrodloDanych; }
        public RodzajPunktu rodzaj() { return _rodzaj; }
        public string operat() { return _operat; }
        public void operat(string sygnaturaDokumentu) { _operat = sygnaturaDokumentu; }
    }
}
