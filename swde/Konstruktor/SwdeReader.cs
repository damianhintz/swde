using System.Text;
using System.IO;

namespace egib.swde.Konstruktor
{
    /// <summary>
    /// Czytnik pliku swde.
    /// ISO 8859-2 – alfabetem SWDE.
    /// Odpowiedzialność: wczytanie pliku swde.
    /// </summary>
    /// <remarks>
    /// Wykorzystany wzorzec budowniczy do konstrukcji modelu.
    /// Separujemy algorytm parsowania/analizy składniowej pliku od tworzenia reprezentacji/modelu samego pliku.
    /// </remarks>
    internal class SwdeReader
    {
        /// <summary>
        /// Strona kodowa pliku SWDE.
        /// </summary>
        public const int CodePage = 28592;

        private StreamReader _reader;
        
        private int _lineNumber;

        /// <summary>
        /// Zwraca liczbę linii w pliku.
        /// </summary>
        public int NumerLinii { get { return _lineNumber; } }

        private string _lineString;

        private BudowniczySwde _budowniczy;

        public SwdeReader(BudowniczySwde budowniczy)
        {
            KontrolerKontekstu.Zapewnij(budowniczy != null, "Do wczytania pliku swde potrzebny jest budowniczy.");
            _budowniczy = budowniczy;
        }

        /// <summary>
        /// Wczytaj z tablicy bajtów.
        /// </summary>
        /// <param name="bytes"></param>
        public void Read(byte[] bytes)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                Read(stream);
            }
        }

        /// <summary>
        /// Wczytaj ze strumienia.
        /// </summary>
        /// <param name="stream"></param>
        public void Read(Stream stream)
        {
            using (_reader = new StreamReader(stream, Encoding.GetEncoding(CodePage)))
            {
                Read(_reader);
            }
        }

        /// <summary>
        /// Wczytaj z pliku.
        /// </summary>
        /// <param name="nazwaPliku"></param>
        public void Read(string nazwaPliku)
        {
            string[] lines = File.ReadAllLines(nazwaPliku, Encoding.GetEncoding(CodePage));
            ReadLines(lines);
        }

        private void Read(StreamReader reader)
        {
            while (JestNastepnaLinia())
            {
                _budowniczy.WczytajLinia(_lineString);
            }
        }

        /// <summary>
        /// Wczytaj z listy linii tekstu.
        /// </summary>
        /// <param name="lines"></param>
        public void ReadLines(params string[] lines)
        {
            for (_lineNumber = 0; _lineNumber < lines.Length; _lineNumber++)
            {
                _lineString = lines[_lineNumber];
                _budowniczy.WczytajLinia(_lineString);
            }
        }

        /// <summary>
        /// Wczytaj kolejną linię ze strumienia.
        /// </summary>
        /// <returns>True jeżeli jest kolejna linia w pliku lub null, jeżeli koniec pliku.</returns>
        private bool JestNastepnaLinia()
        {
            do
            {
                _lineString = _reader.ReadLine();

                if (_lineString == null) return false;

                _lineNumber++;
            } while (string.IsNullOrWhiteSpace(_lineString));

            return true;
        }
    }
}
