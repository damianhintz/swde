using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using egib;
using Pragmatic.Kontrakty;

namespace egib.Testy
{
    [TestClass]
    public class KlasouzytekTest
    {
        FabrykaKlasouzytku klu = new FabrykaKlasouzytku();

        [TestInitialize]
        public void init()
        {
            klu.read(@"..\..\uzytkiG5.csv");
        }

        [TestMethod]
        public void test_ofu_ls()
        {
            Klasouzytek uzytek = new Klasouzytek("Ls", "", "", new Powierzchnia(1));
            Assert.IsTrue(uzytek.jestLs());
        }

        [TestMethod]
        public void test_ozk_ls()
        {
            Klasouzytek uzytek = new Klasouzytek("W", "W", "Ls", new Powierzchnia(1));
            Assert.IsTrue(uzytek.jestLs());
        }

        [TestMethod]
        public void test_ozu_ls()
        {
            Klasouzytek uzytek = new Klasouzytek("W", "Ls", "IV", new Powierzchnia(1));
            Assert.IsTrue(uzytek.jestLs());
        }

        [TestMethod]
        public void test_małe_ofu_nie_ls()
        {
            Klasouzytek uzytek = new Klasouzytek("ls", "", "", new Powierzchnia(1));
            Assert.IsFalse(uzytek.jestLs());
        }

        [TestMethod]
        public void test_oznaczenie_klasoużytku()
        {
            Klasouzytek uzytek = new Klasouzytek("R", "R", "I", new Powierzchnia(1));
            Assert.AreEqual("RI", uzytek.oznaczenie(klu));
            Assert.IsNotNull(uzytek.powierzchnia());
        }

        [TestMethod]
        public void test_powierzchnia_klasoużytku()
        {
            long m2 = 1234;
            Klasouzytek uzytek = new Klasouzytek("R", "R", "I", new Powierzchnia(m2));
            Assert.IsNotNull(uzytek.powierzchnia());
            Assert.AreEqual(m2, uzytek.powierzchnia().metryKwadratowe());
        }

        [TestMethod]
        public void test_jest_zabudowany_BRI()
        {
            Assert.IsTrue(new Klasouzytek("B", "R", "I", new Powierzchnia(1)).jestZabudowany());
        }

        [TestMethod]
        public void test_jest_zabudowany_ofu()
        {
            Assert.IsTrue(new Klasouzytek("B", "", "", new Powierzchnia(1)).jestZabudowany());
        }

        [TestMethod]
        public void test_nie_jest_zabudowany()
        {
            Assert.IsFalse(new Klasouzytek("dr", "", "", new Powierzchnia(1)).jestZabudowany());
        }

        [TestMethod]
        public void test_jest_zabudowany_ozu_B()
        {
            Assert.IsTrue(new Klasouzytek("", "B", "", new Powierzchnia(1)).jestZabudowany());
        }
        
        [TestMethod]
        public void test_br_ozk()
        {
            Klasouzytek uzytek = new Klasouzytek("Br", "Br", "Br", new Powierzchnia(1));
            Assert.AreEqual("Br", uzytek.ozk());
        }

        [TestMethod]
        public void test_br_ozu()
        {
            Klasouzytek uzytek = new Klasouzytek("Br", "Br", "V", new Powierzchnia(1));
            Assert.AreEqual("Br", uzytek.ozu());
        }

        [TestMethod]
        public void test_br_ofu()
        {
            Klasouzytek uzytek = new Klasouzytek("Br", "R", "V", new Powierzchnia(1));
            Assert.AreEqual("Br", uzytek.ofu());
        }

        [TestMethod]
        public void test_br_oznaczenie()
        {
            Klasouzytek uzytek = new Klasouzytek("Br", "R", "V", new Powierzchnia(1));
            Assert.AreEqual("B/RV", uzytek.oznaczenie(klu));
        }

        [TestMethod]
        public void test_tylko_ofu()
        {
            Klasouzytek uzytek = new Klasouzytek("dr", "", "", new Powierzchnia(1));
            Assert.AreEqual("dr", uzytek.ofu());
            Assert.AreEqual("", uzytek.ozu());
            Assert.AreEqual("", uzytek.ozk());
        }

        [TestMethod]
        public void test_tylko_ofu_ekologiczne()
        {
            Klasouzytek uzytek = new Klasouzytek("E-Lz", "", "", new Powierzchnia(1));
            Assert.AreEqual("E-Lz", uzytek.ofu());
            Assert.AreEqual("", uzytek.ozu());
            Assert.AreEqual("", uzytek.ozk());
        }

        [TestMethod]
        public void test_brak_ofu()
        {
            Klasouzytek uzytek = new Klasouzytek("", "LzR", "Va", new Powierzchnia(1));
            Assert.AreEqual("", uzytek.ofu());
            Assert.AreEqual("LzR", uzytek.ozu());
            Assert.AreEqual("Va", uzytek.ozk());
        }

        [TestMethod]
        public void test_ofu_ekologiczne()
        {
            Klasouzytek uzytek = new Klasouzytek("E-Lz", "R", "II", new Powierzchnia(1));
            Assert.AreEqual("E-Lz", uzytek.ofu());
            Assert.AreEqual("R", uzytek.ozu());
            Assert.AreEqual("II", uzytek.ozk());
        }
    }
}
