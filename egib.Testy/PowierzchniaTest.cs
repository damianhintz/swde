using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pragmatic.Kontrakty;
using egib;

namespace egib.Testy
{
    [TestClass]
    public class PowierzchniaTest
    {
        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestPustaPowierzchnia()
        {
            Powierzchnia.parseMetry(String.Empty);
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestUjemnaPowierzchnia()
        {
            Powierzchnia.parseMetry("-100");
        }

        [TestMethod, ExpectedException(typeof(KontraktPreException))]
        public void TestZerowaPowierzchnia()
        {
            Powierzchnia.parseMetry("0");
        }

        [TestMethod, ExpectedException(typeof(KontraktAssertException))]
        public void TestZmiennoprzecinkowaPowierzchnia()
        {
            Powierzchnia.parseMetry("0.1");
        }

        [TestMethod]
        public void TestMetryKwadratowe()
        {
            double expectedMetry = 10000.0;
            Powierzchnia pow = new Powierzchnia((long)expectedMetry);
            Assert.AreEqual(expectedMetry, pow.metryKwadratowe(), 0.0001);
        }

        [TestMethod]
        public void test_powierzchnia_ary()
        {
            double metry = 10000.0;
            double expectedAry = 100.0;
            Powierzchnia pow = new Powierzchnia((long)metry);
            Assert.AreEqual(expectedAry, pow.ary(), 0.0001);
        }

        [TestMethod]
        public void test_powierzchnia_hektary()
        {
            double metry = 10000.0;
            double expectedHektary = 1.0;
            Powierzchnia pow = new Powierzchnia((long)metry);
            Assert.AreEqual(expectedHektary, pow.hektary(), 0.0001);
        }

        [TestMethod]
        public void test_domyślny_format_hektary()
        {
            Powierzchnia pow = new Powierzchnia(10000);
            Assert.AreEqual("1.0000", pow.ToString());
        }

        [TestMethod]
        public void test_powierzchnia_parse_hektary()
        {
            Powierzchnia pow = Powierzchnia.parseHektary("1.0000");
            Assert.AreEqual(10000, pow.metryKwadratowe());
        }
        
        [TestMethod]
        public void test_powierzchnia_do_ara_200()
        {
            Powierzchnia pow = new Powierzchnia(200);
            Assert.AreEqual(2, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_151()
        {
            Powierzchnia pow = new Powierzchnia(151);
            Assert.AreEqual(2, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_150()
        {
            Powierzchnia pow = new Powierzchnia(150);
            Assert.AreEqual(2, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_149()
        {
            Powierzchnia pow = new Powierzchnia(149);
            Assert.AreEqual(1, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_101()
        {
            Powierzchnia pow = new Powierzchnia(101);
            Assert.AreEqual(1, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_100()
        {
            Powierzchnia pow = new Powierzchnia(100);
            Assert.AreEqual(1, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_99()
        {
            Powierzchnia pow = new Powierzchnia(99);
            Assert.AreEqual(1, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_ara_1()
        {
            Powierzchnia pow = new Powierzchnia(1);
            Assert.AreEqual(1, pow.metryDoAra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_metra_1()
        {
            Assert.AreEqual(100, new Powierzchnia(1).aryDoMetra().metryKwadratowe());
        }

        [TestMethod]
        public void test_powierzchnia_do_metra_2()
        {
            Assert.AreEqual(200, new Powierzchnia(2).aryDoMetra().metryKwadratowe());
        }
    }
}
