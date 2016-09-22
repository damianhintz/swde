using swde.Sekcje;
using swde.Rekordy;

namespace swde.Komponenty
{
    class TerminatorSwde : KomponentBase
    {
        public override bool JestTerminatorem(KomponentBase symbol)
        {
            return symbol is DokumentBase;
        }
    }

    class TerminatorSekcjiSwde : KomponentBase
    {
        public override bool JestTerminatorem(KomponentBase symbol)
        {
            return symbol is SekcjaSwdeBase;
        }
    }

    class TerminatorRekorduLubTypuSwde : KomponentBase
    {
        public override bool JestTerminatorem(KomponentBase symbol)
        {
            return symbol is RekordSwdeBase || symbol is DefinicjaTypuSwde;
        }
    }

    class TerminatorLiniiLubObszaruSwde : KomponentBase
    {
        public override bool JestTerminatorem(KomponentBase symbol)
        {
            return symbol is RekordLiniaSwde || symbol is RekordObszarSwde;
        }
    }
}
