using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class PunktGranicznyTest
    {
        [TestMethod]
        public void test_puste_źródło_danych()
        {
            string zrd = String.Empty;
            PunktGraniczny punkt = new PunktGraniczny(zrd, String.Empty);
            Assert.AreEqual(zrd, punkt.zrodloDanych());
            Assert.IsTrue(punkt.rodzaj().nieznany());
        }

        [TestMethod]
        public void test_nieznane_źródło_danych()
        {
            string zrd = "zrdNieznane";
            PunktGraniczny punkt = new PunktGraniczny(zrd, String.Empty);
            Assert.AreEqual(zrd, punkt.zrodloDanych());
            Assert.IsTrue(punkt.rodzaj().nieznany());
        }

        [TestMethod]
        public void test_źródło_danych_wektoryzacja()
        {
            string[] zrdList = new string[] { "2", "3", "4", "6", "7", "8", "9" };
            foreach (var zrd in zrdList)
            {
                PunktGraniczny punkt = new PunktGraniczny(zrd, "1");
                Assert.AreEqual(zrd, punkt.zrodloDanych());
                Assert.IsTrue(punkt.rodzaj().zWektoryzacji());
            }
        }

        [TestMethod]
        public void test_źródło_danych_pomiar()
        {
            string[] zrdList = new string[] { "1", "5" };
            foreach (var zrd in zrdList)
            {
                PunktGraniczny punkt = new PunktGraniczny(zrd, "1");
                Assert.AreEqual(zrd, punkt.zrodloDanych());
                Assert.IsTrue(punkt.rodzaj().zPomiaru());
            }
        }

        [TestMethod]
        public void test_rodzaj_dla_zrd_0()
        {
            string zrd = "0";
            PunktGraniczny punkt = new PunktGraniczny(zrd, "0");
            Assert.AreEqual(zrd, punkt.zrodloDanych());
            Assert.IsNotNull(punkt.rodzaj());
            Assert.IsTrue(punkt.rodzaj().nieznany());
        }
    }
}
