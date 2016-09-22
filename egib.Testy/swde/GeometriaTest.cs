using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using egib.swde;

namespace egib.Testy.swde
{
    [TestClass]
    public class GeometriaTest
    {
        private string _fileName = Path.Combine(
                Environment.CurrentDirectory,
                @"..\..\swde\GeometriaTest.txt");
        private DokumentSwde _dokument;

        [TestInitialize]
        public void init()
        {
            _dokument = new DokumentSwde(_fileName);
        }

        [TestMethod]
        public void test_swde_geometria()
        {
            Assert.AreEqual(3, _dokument.Count);
        }
    }
}
