using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pragmatic.Kontrakty;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class IdentyfikatorDzialkiTest
    {
        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestPustyObreb()
        {
            IdentyfikatorDzialki id = new IdentyfikatorDzialki(String.Empty, "1");
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestPustaDzialka()
        {
            IdentyfikatorDzialki id = new IdentyfikatorDzialki("1", String.Empty);
        }

        [TestMethod]
        public void TestValue()
        {
            string expectedObreb = "1";
            string expectedDzialka = "1003";
            string expectedId = expectedObreb + "-" + expectedDzialka;
            IdentyfikatorDzialki id = new IdentyfikatorDzialki(expectedObreb, expectedDzialka);
            Assert.AreEqual(expectedId, id.identyfikator());
            Assert.AreEqual(expectedId, id.ToString());
            Assert.AreEqual(expectedObreb, id.numerObrebu());
            Assert.AreEqual(expectedDzialka, id.numerDzialki());
        }

        [TestMethod]
        public void TestParseId()
        {
            string expected = "10-10/2";
            IdentyfikatorDzialki id = IdentyfikatorDzialki.parseId(expected);
            Assert.AreEqual(expected, id.identyfikator());
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestParseIdPuste()
        {
            IdentyfikatorDzialki.parseId(String.Empty);
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestParseIdTylkoSeparator()
        {
            IdentyfikatorDzialki.parseId("-");
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestParseIdTylkoDzialka()
        {
            IdentyfikatorDzialki.parseId("10/2");
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestParseIdTylkoSeparatorDzialka()
        {
            IdentyfikatorDzialki.parseId("-10/2");
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestParseIdTylkoObreb()
        {
            IdentyfikatorDzialki.parseId("1-");
        }

        [TestMethod]
        public void TestParseG5()
        {
            IdentyfikatorDzialki id = IdentyfikatorDzialki.parseG5("142302_2.0001.296/1");
            Assert.AreEqual("1", id.numerObrebu());
            Assert.AreEqual("296/1", id.numerDzialki());
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void TestParseG5NumerObrebuZaDuzy()
        {
            IdentyfikatorDzialki.parseG5("142302_2.1234.4321");
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void TestParseG5BrakObrebu()
        {
            IdentyfikatorDzialki.parseG5("142302_2..296");
        }
    }
}
