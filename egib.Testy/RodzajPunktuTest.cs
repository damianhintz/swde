using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class RodzajPunktuTest
    {
        [TestMethod]
        public void test_rodzaj_nieznane_bpp_i_zrd()
        {
            RodzajPunktu rodzaj = new RodzajPunktu(String.Empty, String.Empty);
            Assert.IsTrue(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("nieznany", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_nieznane_bpp()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("1", String.Empty);
            Assert.IsTrue(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("nieznany", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_nieznane_bpp_powyżej_6()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("1", "7");
            Assert.IsTrue(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("nieznany", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_nieznane_zrd()
        {
            RodzajPunktu rodzaj = new RodzajPunktu(String.Empty, "1");
            Assert.IsTrue(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("nieznany", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_wektoryzacja_zrd()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("2", "1");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsTrue(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("z wektoryzacji", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_wektoryzacja_bpp()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("1", "2");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsTrue(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("z wektoryzacji", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_wektoryzacja_bpp_powyżej_3()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("1", "6");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsTrue(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("z wektoryzacji", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_wektoryzacja_bpp_i_zrd()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("2", "2");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsTrue(rodzaj.zWektoryzacji());
            Assert.IsFalse(rodzaj.zPomiaru());
            Assert.AreEqual("z wektoryzacji", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_pomiar_zrd1()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("1", "1");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsTrue(rodzaj.zPomiaru());
            Assert.AreEqual("z operatu", rodzaj.ToString());
        }

        [TestMethod]
        public void test_rodzaj_pomiar_zrd5()
        {
            RodzajPunktu rodzaj = new RodzajPunktu("5", "1");
            Assert.IsFalse(rodzaj.nieznany());
            Assert.IsFalse(rodzaj.zWektoryzacji());
            Assert.IsTrue(rodzaj.zPomiaru());
            Assert.AreEqual("z operatu", rodzaj.ToString());
        }
    }
}
