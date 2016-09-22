using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace egib.swde
{
    /// <summary>
    /// Wyjątek rzucany przez SwdeReader, kiedy wystąpi błąd parsowania pliku SWDE.
    /// </summary>
    class ParseSwdeException : ApplicationException
    {
        public int LineNumber { get; set; }
        public string LineString { get; set; }

        public ParseSwdeException(string message, int lineNumber, string lineString)
            : base(message)
        {
            LineNumber = lineNumber;
            LineString = lineString;
        }

        public ParseSwdeException(string message, int lineNumber, string lineString, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
