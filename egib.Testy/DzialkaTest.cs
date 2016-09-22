using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace egib.Testy
{
    [TestClass]
    public class DzialkaTest
    {
        RodzajDzialki rodzajDzialki = new RodzajDzialki();
        DzialkaEwidencyjna dzialka = new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "1"),
                new Powierzchnia(100));

        [TestMethod]
        public void test_powierzchnia_analityczna()
        {
            Assert.IsNotNull(dzialka.powierzchnia());
        }

        [TestMethod]
        public void test_powierzchnia_ewidencyjna_niezdefiniowana()
        {
            Assert.IsFalse(dzialka.przypisanaDotychczasowa());
        }

        [TestMethod]
        public void TestPowierzchniaEwidencyjnaZdefiniowana()
        {
            DzialkaEwidencyjna ewid = new DzialkaEwidencyjna(dzialka.identyfikator(), dzialka.powierzchnia());
            ewid.jednostkaRejestrowa(JednostkaRejestrowa.parseG5("142307_2.0001.G00001"));
            ewid.dodajKlasouzytek(new Klasouzytek("dr", "", "", new Powierzchnia(1)));
            dzialka.dzialkaDotychczasowa(ewid);
            Assert.IsTrue(dzialka.przypisanaDotychczasowa());
        }

        [TestMethod]
        public void TestDodajJedenKlasouzytek()
        {
            Klasouzytek uzytek = new Klasouzytek("Ps", "Ps", "I", new Powierzchnia(1));
            dzialka.dodajKlasouzytek(uzytek);
            Assert.IsTrue(dzialka.countKlasouzytki() == 1);
        }

        [TestMethod]
        public void test_rodzaj_nieznany_bez_punktów()
        {
            Assert.AreEqual(0, dzialka.countPunkty());
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
        }

        [TestMethod]
        public void test_jeden_nieznany_punkt()
        {
            dzialka.dodajPunkt(new PunktGraniczny(String.Empty, String.Empty));
            Assert.AreEqual(1, dzialka.countPunkty());
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
        }

        [TestMethod]
        public void test_jeden_punkt_z_wektoryzacji()
        {
            dzialka.dodajPunkt(new PunktGraniczny("3", "3"));
            Assert.AreEqual(1, dzialka.countPunkty());
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
        }

        [TestMethod]
        public void test_jeden_punkt_z_pomiaru()
        {
            dzialka.dodajPunkt(new PunktGraniczny("5", "1"));
            Assert.AreEqual(1, dzialka.countPunkty());
            Assert.IsTrue(rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_trzy_punkty_nieznany_wektoryzacja_pomiar()
        {
            dzialka.dodajPunkt(new PunktGraniczny("1", "1"));
            dzialka.dodajPunkt(new PunktGraniczny("3", "3"));
            dzialka.dodajPunkt(new PunktGraniczny(String.Empty, String.Empty));
            Assert.AreEqual(3, dzialka.countPunkty());
            //Assert.AreEqual(1, _dzialka.countPunktyNieznane());
            //Assert.AreEqual(1, _dzialka.countPunktyWektoryzacja());
            //Assert.AreEqual(1, _dzialka.countPunktyOperat());
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
        }

        [TestMethod]
        public void test_dwa_punkty_wektoryzacja_pomiar()
        {
            dzialka.dodajPunkt(new PunktGraniczny("1", "1"));
            Assert.IsTrue(rodzajDzialki.pomierzona(dzialka));
            Assert.AreEqual(1, dzialka.countPunkty());
            //Assert.AreEqual(1, _dzialka.countPunktyOperat());
            dzialka.dodajPunkt(new PunktGraniczny("3", "3"));
            Assert.AreEqual(2, dzialka.countPunkty());
            //Assert.AreEqual(1, _dzialka.countPunktyWektoryzacja());
            //Assert.AreEqual(1, _dzialka.countPunktyOperat());
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
        }

        [TestMethod]
        public void test_dwa_punkty_nieznany_pomiar()
        {
            dzialka.dodajPunkt(new PunktGraniczny("1", "1"));
            dzialka.dodajPunkt(new PunktGraniczny(String.Empty, String.Empty));
            Assert.AreEqual(2, dzialka.countPunkty());
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
        }

        [TestMethod]
        public void TestPowierzchniaJednegoUzytku()
        {
            long powUzytku = 1;
            dzialka.dodajKlasouzytek(new Klasouzytek("a", "", "", new Powierzchnia(powUzytku)));
            Assert.AreEqual(1, dzialka.countKlasouzytki());
            Assert.AreEqual(powUzytku, dzialka.powierzchniaUzytkow().metryKwadratowe());
        }

        [TestMethod]
        public void TestPowierzchniaWieluKlasouzytkow()
        {
            long powUzytku = 1;
            int countUzytki = 10;
            for (int i = 0; i < countUzytki; i++)
            {
                dzialka.dodajKlasouzytek(new Klasouzytek("a" + i, "", "", new Powierzchnia(powUzytku)));
            }
            Assert.AreEqual(countUzytki, dzialka.countKlasouzytki());
            Assert.AreEqual(powUzytku * countUzytki, dzialka.powierzchniaUzytkow().metryKwadratowe());
        }
    }
}
