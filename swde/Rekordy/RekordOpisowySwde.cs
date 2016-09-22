namespace swde.Rekordy
{
    /// <summary>
    /// Rekord nie posiada odniesienia przestrzennego.
    /// </summary>
    internal class RekordOpisowySwde : RekordSwdeBase
    {
        public override string TypBazowy
        {
            get
            {
                return "RD";
            }
        }

        public RekordOpisowySwde(string kod, string typ, string id, string idr, string st_obj) :
            base(kod, typ, id, idr, st_obj)
        {
        }
    }
}
