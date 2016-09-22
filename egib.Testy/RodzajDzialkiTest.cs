using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pragmatic.Kontrakty;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class RodzajDzialkiTest
    {
        RodzajDzialki rodzajDzialki;
        DzialkaEwidencyjna dzialka;
        PunktGraniczny punktNieznany = new PunktGraniczny("", "");
        PunktGraniczny punktWektoryzacja = new PunktGraniczny("2", "1");
        PunktGraniczny punktPomiar = new PunktGraniczny("5", "1");
        PunktGraniczny punktOperat = new PunktGraniczny("1", "1");
        
        [TestInitialize]
        public void init_test()
        {
            string[] operaty = new string[] { "opa", "opb", "opc" };
            rodzajDzialki = new OperatowyRodzajDzialki(operaty);
            punktNieznany.operat("opc");
            punktWektoryzacja.operat("opb");
            punktPomiar.operat("opx");
            punktOperat.operat("opa");
            dzialka = new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "12/3"),
                new Powierzchnia(1234));
        }

        [TestMethod]
        public void test_działka_nieznana()
        {
            dzialka.dodajPunkt(punktNieznany);
            dzialka.dodajPunkt(punktNieznany);
            dzialka.dodajPunkt(punktNieznany);
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(!rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_nieznana_mieszane()
        {
            dzialka.dodajPunkt(punktWektoryzacja);
            dzialka.dodajPunkt(punktNieznany);
            dzialka.dodajPunkt(punktOperat);
            Assert.IsTrue(rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(!rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_wektoryzacja()
        {
            dzialka.dodajPunkt(punktWektoryzacja);
            dzialka.dodajPunkt(punktWektoryzacja);
            dzialka.dodajPunkt(punktWektoryzacja);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_wektoryzacja_mieszane()
        {
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktWektoryzacja);
            dzialka.dodajPunkt(punktOperat);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_wektoryzacja_pomierzone()
        {
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_pomiar()
        {
            dzialka.dodajPunkt(punktOperat);
            dzialka.dodajPunkt(punktOperat);
            dzialka.dodajPunkt(punktOperat);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(!rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_pomiar_ewidencyjna_100()
        {
            DzialkaEwidencyjna dotychczasowa = new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "2/3"),
                new Powierzchnia(100));
            dotychczasowa.jednostkaRejestrowa(JednostkaRejestrowa.parseG5("142307_2.0001.G00001"));
            dotychczasowa.dodajKlasouzytek(new Klasouzytek("W", "", "", new Powierzchnia(100)));
            dzialka.dzialkaDotychczasowa(dotychczasowa);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(!rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_pomiar_ewidencyjna_10010()
        {
            DzialkaEwidencyjna dotychczasowa = new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "2/3"),
                new Powierzchnia(10010));
            dotychczasowa.jednostkaRejestrowa(JednostkaRejestrowa.parseG5("142307_2.0001.G00001"));
            dotychczasowa.dodajKlasouzytek(new Klasouzytek("W", "", "", new Powierzchnia(100)));
            dzialka.dzialkaDotychczasowa(dotychczasowa);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktPomiar);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }

        [TestMethod]
        public void test_działka_mieszane_ewidencyjna_00()
        {
            DzialkaEwidencyjna dotychczasowa = new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "2/3"),
                new Powierzchnia(100));
            dotychczasowa.jednostkaRejestrowa(JednostkaRejestrowa.parseG5("142307_2.0001.G00001"));
            dotychczasowa.dodajKlasouzytek(new Klasouzytek("W", "", "", new Powierzchnia(100)));
            dzialka.dzialkaDotychczasowa(dotychczasowa);
            dzialka.dodajPunkt(punktPomiar);
            dzialka.dodajPunkt(punktWektoryzacja);
            dzialka.dodajPunkt(punktOperat);
            Assert.IsTrue(!rodzajDzialki.nieznany(dzialka));
            Assert.IsTrue(rodzajDzialki.niepomierzona(dzialka));
            Assert.IsTrue(!rodzajDzialki.pomierzona(dzialka));
        }
    }
}
