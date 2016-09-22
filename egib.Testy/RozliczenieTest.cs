using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Pragmatic.Kontrakty;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class RozliczenieTest
    {
        Rozliczenie rozliczenie = new Rozliczenie(new RodzajDzialki(), null);

        [TestMethod]
        public void TestNiezaimportowaneRozliczenieNiemaDzialek()
        {
            Assert.IsTrue(rozliczenie.Count() == 0);
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestDodajPustaDzialka()
        {
            rozliczenie.dodajDzialka(null);
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestDodajDzialkaBezUzytku()
        {
            rozliczenie.dodajDzialka(new DzialkaEwidencyjna(
                new IdentyfikatorDzialki("1", "1"),
                new Powierzchnia(100)));
        }

        [TestMethod]
        public void TestDodajJednaDzialka()
        {
            IdentyfikatorDzialki id = new IdentyfikatorDzialki("1", "1");
            DzialkaEwidencyjna dzialka = new DzialkaEwidencyjna(id, new Powierzchnia(10));
            Klasouzytek klasouzytek = new Klasouzytek("R", "R", "I", new Powierzchnia(10));
            dzialka.dodajKlasouzytek(klasouzytek);
            rozliczenie.dodajDzialka(dzialka);
            Assert.AreEqual(1, rozliczenie.Count());
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestDodajPowtorzonaDzialka()
        {
            IdentyfikatorDzialki id1 = new IdentyfikatorDzialki("1", "1");
            DzialkaEwidencyjna dzialka1 = new DzialkaEwidencyjna(id1, new Powierzchnia(10));
            Klasouzytek uzytek = new Klasouzytek("R", "R", "I", new Powierzchnia(10));
            dzialka1.dodajKlasouzytek(uzytek);
            rozliczenie.dodajDzialka(dzialka1);
            IdentyfikatorDzialki id2 = new IdentyfikatorDzialki("1", "1");
            DzialkaEwidencyjna dzialka2 = new DzialkaEwidencyjna(id2, new Powierzchnia(10));
            dzialka2.dodajKlasouzytek(uzytek);
            rozliczenie.dodajDzialka(dzialka2);
        }

        [TestMethod]
        public void TestWyszukajId()
        {
            IdentyfikatorDzialki id = new IdentyfikatorDzialki("1", "1");
            DzialkaEwidencyjna dzialka = new DzialkaEwidencyjna(id, new Powierzchnia(10));
            Klasouzytek klasouzytek = new Klasouzytek("R", "R", "I", new Powierzchnia(10));
            dzialka.dodajKlasouzytek(klasouzytek);
            rozliczenie.dodajDzialka(dzialka);
            Assert.IsTrue(rozliczenie.zawieraId(id));
            Assert.IsNotNull(rozliczenie.dzialkaById(id));
        }
    }
}
