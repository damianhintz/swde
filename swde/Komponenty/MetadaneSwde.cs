namespace swde.Komponenty
{
    internal class MetadaneSwde : KomponentBase
    {
        protected bool _uzytkownika;
        
        protected string _nazwa;
        
        /// <summary>
        /// Zwraca Nazwę metadanej.
        /// </summary>
        public string Nazwa { get { return this[_nazwa]; } }

        protected string _wartosc;

        /// <summary>
        /// Zwraca wartość metadanej.
        /// </summary>
        public string Wartosc { get { return _wartosc; } }

        public MetadaneSwde(string nazwa, string wartosc, bool uzytkownika = false)
        {
            _uzytkownika = uzytkownika;
            _nazwa = nazwa;
            _wartosc = wartosc;
        }

        private string this[string nazwa]
        {
            get
            {
                switch (nazwa)
                {
                    //Administracyjne
                    case "TR": //nr identyfikacyjny jednostki tworzącej plik.
                        return "Numer jednostki tworzącej plik";
                    case "TN": //nazwa jednostki tworzącej plik.
                        return "Nazwa jednostki tworzącej plik";
                    case "TA": //adres jednostki tworzącej plik.
                        return "Adres jednostki tworzącej plik";
                    case "TO": //imię i nazwisko wykonawcy.
                        return "Imię i nazwisko wykonawcy";
                    case "ZN": //nazwa systemu - źródła danych.
                        return "Nazwa systemu";
                    case "ZR": //nr identyfikacyjny systemu - źródła danych.
                        return "Numer identyfikacyjny systemu";
                    case "ZD": //nazwa zbioru danych - reprezentowanego obiektu.
                        return "Nazwa zbioru danych";
                    case "OP": //przeznaczenie danych.
                        return "Przeznaczenie danych";
                    case "OR": //nr identyfikacyjny jednostki przeznaczenia.
                        return "Numer jednostki przeznaczenia";
                    case "ON": //nazwa jednostki przeznaczenia.
                        return "Nazwa jednostki przeznaczenia";
                    case "OA": //adres jednostki przeznaczenia.
                        return "Adres jednostki przeznaczenia";
                    case "OO": //imię i nazwisko odbiorcy.
                        return "Imię i nazwisko odbiorcy";
                    case "DN": //Data utworzenia pliku.
                        return "Data utworzenia pliku";
                    //Geodezyjne
                    case "UX": //nazwa układu współrzędnych.
                        return "Nazwa układu współrzędnych";
                    case "OS": //nazwa / numer strefy odwzorwania.
                        return "Nazwa / numer strefy odwzorwania";
                    case "NX": //nazwa pierwszej współrzędnej.
                        return "Nazwa pierwszej współrzędnej";
                    case "NY": //nazwa drugiej współrzędnej.
                        return "Nazwa drugiej współrzędnej";
                    case "NZ": //nazwa trzeciej współrzędnej.
                        return "Nazwa trzeciej współrzędnej";
                    case "UH": //system wysokości.
                        return "System wysokości";
                    case "HZ": //poziom odniesienia.
                        return "Poziom odniesienia";
                    default:
                        return nazwa;
                }
            }
        }
    }
}
