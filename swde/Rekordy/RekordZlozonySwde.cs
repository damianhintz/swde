namespace egib.swde.Rekordy
{
    /// <summary>
    /// Rekord, który przedstawia obiekt złożony z innych obiektów.
    /// W formacie SWDE to rekord składowy (element) wskazuje na rekord obiektu złożonego.
    /// </summary>
    internal class RekordZlozonySwde : RekordSwdeBase
    {
        public override string TypBazowy
        {
            get
            {
                return "RC";
            }
        }

        public RekordZlozonySwde(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
        }
    }
}
