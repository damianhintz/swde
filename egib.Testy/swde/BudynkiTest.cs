using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using swde;

namespace egib.Testy.swde
{
    [TestClass]
    public class BudynkiTest
    {
        private string _fileName = Path.Combine(
                Environment.CurrentDirectory,
                @"..\..\swde\BudynkiTest.txt");
        private DokumentSwde _dokument;

        [TestInitialize]
        public void init()
        {
            _dokument = new DokumentSwde(_fileName);
        }

        [TestMethod]
        public void test_zlicz_budynki()
        {
            int count = 0;
            foreach(var obiekt in _dokument.GetObiekty())
            {
                if (obiekt.Typ.Contains("BUD")) count++;
            }
            Assert.AreEqual(1, count);
            IEnumerable<ObiektSwde> budynki = _dokument.GetObiektyKlasy("G5BUD");
            Assert.AreEqual(0, LoggerSwde.Bledy);
            Assert.AreEqual(1, budynki.Count());
        }
    }
}
