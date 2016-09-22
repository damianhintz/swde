using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using swde;

namespace egib.Testy.swde
{
    [TestClass]
    public class DokumentTest
    {
        private string _fileName = Path.Combine(
                Environment.CurrentDirectory,
                @"..\..\swde\DokumentTest.txt");
        private DokumentSwde _dokument;

        [TestInitialize]
        public void init()
        {
            _dokument = new DokumentSwde(_fileName);
        }

        [TestMethod]
        public void test_zlicz_wszystkie()
        {
            Assert.AreEqual(16, _dokument.Count);
        }
        
        [TestMethod]
        public void test_dokument()
        {
            ObiektSwde obiekt = obiektKlasy("G5DOK");
            Assert.AreEqual("oznaczenie testowe", obiekt.GetAtrybut("G5IDM"));
            Assert.AreEqual("16", obiekt.GetAtrybut("G5KDK"));
            Assert.AreEqual("2011.05.13", obiekt.GetAtrybut("G5DTD"));
            Assert.AreEqual("2013.05.13", obiekt.GetAtrybut("G5DTP"));
            Assert.AreEqual("sygnatura testowa", obiekt.GetAtrybut("G5SYG"));
            Assert.AreEqual("testowa nazwa sądu rejonowego", obiekt.GetAtrybut("G5NSR"));
            Assert.AreEqual("testowy opis", obiekt.GetAtrybut("G5OPD"));
            Assert.AreEqual("2013.01.01-01:00:00", obiekt.GetAtrybut("G5DTW"));
            Assert.AreEqual("2013.01.01-01:00:00", obiekt.GetAtrybut("G5DTU"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNull(obiekt.GetRelacjaLubNull("G5RDOK"));
        }

        [TestMethod]
        public void test_adres()
        {
            ObiektSwde obiekt = obiektKlasy("G5ADR");
            Assert.AreEqual("ulica;kod pocztowy;miejscowosc", obiekt.GetAtrybut("G5NAZ"));
            Assert.AreEqual("Polska", obiekt.GetAtrybut("G5KRJ"));
            Assert.AreEqual("woj.warmińskow-mazurskie", obiekt.GetAtrybut("G5WJD"));
            Assert.AreEqual("pow.olsztyński", obiekt.GetAtrybut("G5PWJ"));
            Assert.AreEqual("gm.olsztyn", obiekt.GetAtrybut("G5GMN"));
            Assert.AreEqual("ul.testowa", obiekt.GetAtrybut("G5ULC"));
            Assert.AreEqual("1", obiekt.GetAtrybut("G5NRA"));
            Assert.AreEqual("7", obiekt.GetAtrybut("G5NRL"));
            Assert.AreEqual("olsztyn", obiekt.GetAtrybut("G5MSC"));
            Assert.AreEqual("00-000", obiekt.GetAtrybut("G5KOD"));
            Assert.IsNull(obiekt.Geometria);
        }

        [TestMethod]
        public void test_jednostka_ewidencyjna()
        {
            ObiektSwde obiekt = obiektKlasy("G5JEW");
            Assert.AreEqual("142401_2", obiekt.GetAtrybut("G5IDJ"));
            Assert.AreEqual("104494573", obiekt.GetAtrybut("G5PEW"));
            Assert.AreEqual("Gzy", obiekt.GetAtrybut("G5NAZ"));
            Assert.AreEqual("2013.01.01-12:00:00.00000", obiekt.GetAtrybut("G5DTW"));
            Assert.AreEqual("2013.01.01-12:00:00.00000", obiekt.GetAtrybut("G5DTU"));
            Assert.IsNotNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RKRG"));
        }
        
        [TestMethod]
        public void test_obręb_ewidencyjny()
        {
            ObiektSwde obiekt = obiektKlasy("G5OBR");
            Assert.AreEqual("142401_2.0032", obiekt.GetAtrybut("G5NRO"));
            Assert.AreEqual("1563114", obiekt.GetAtrybut("G5PEW"));
            Assert.AreEqual("obręb testowy", obiekt.GetAtrybut("G5NAZ"));
            Assert.IsNotNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RKRG"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RJEW"));
        }

        [TestMethod]
        public void test_kontur_użytku_gruntowego()
        {
            ObiektSwde obiekt = obiektKlasy("G5UZG");
        }

        [TestMethod]
        public void test_osoba_fizyczna()
        {
            ObiektSwde obiekt = obiektKlasy("G5OSF");
            Assert.AreEqual("1", obiekt.GetAtrybut("G5STI"));
            Assert.AreEqual("2", obiekt.GetAtrybut("G5PLC"));
            Assert.AreEqual("66051512847", obiekt.GetAtrybut("G5PSL"));
            Assert.AreEqual("123456789", obiekt.GetAtrybut("G5NIP"));
            Assert.AreEqual("MYZIAK-DRUGA", obiekt.GetAtrybut("G5NZW"));
            Assert.AreEqual("IRENA", obiekt.GetAtrybut("G5PIM"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
        }

        [TestMethod]
        public void test_instytucja()
        {
            ObiektSwde obiekt = obiektKlasy("G5INS");
            Assert.AreEqual("31", obiekt.GetAtrybut("G5STI"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
        }

        [TestMethod]
        public void test_małżeństwo()
        {
            ObiektSwde obiekt = obiektKlasy("G5MLZ");
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RŻONA"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RMĄŻ"));
            Assert.AreSame(obiekt.GetRelacjaLubNull("G5RŻONA"), obiekt.GetRelacjaLubNull("G5RMĄŻ"));
        }

        [TestMethod]
        public void test_inny_podmiot_grupowy()
        {
            ObiektSwde obiekt = obiektKlasy("G5OSZ");
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RSKD"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RSZD"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
        }
        
        [TestMethod]
        public void test_jednostka_rejestrowa()
        {
            ObiektSwde obiekt = obiektKlasy("G5JDR");
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5ROBR"));
        }

        [TestMethod]
        public void test_udział_własności_lub_władania()
        {
            ObiektSwde obiekt = obiektKlasy("G5UDZ");
            Assert.AreEqual("1/1", obiekt.GetAtrybut("G5UD"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RWŁS"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPOD"));
        }

        [TestMethod]
        public void test_udział_we_władaniu()
        {
            ObiektSwde obiekt = obiektKlasy("G5UDW");
            Assert.AreEqual("6", obiekt.GetAtrybut("G5RWD"));
            Assert.AreEqual("600/33100", obiekt.GetAtrybut("G5UD"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RWŁD"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPOD"));
        }

        [TestMethod]
        public void test_działka_ewidencyjna()
        {
            ObiektSwde obiekt = obiektKlasy("G5DZE");
            Assert.AreEqual("142401_2.0013.23/2", obiekt.GetAtrybut("G5IDD"));
            Assert.AreEqual("3284", obiekt.GetAtrybut("G5PEW"));
            Assert.IsNotNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPWŁ"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPWD"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RKRG"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RJDR"));
        }

        [TestMethod]
        public void test_klasoużytek()
        {
            ObiektSwde obiekt = obiektKlasy("G5KLU");
            Assert.AreEqual("R", obiekt.GetAtrybut("G5OFU"));
            Assert.AreEqual("R", obiekt.GetAtrybut("G5OZU"));
            Assert.AreEqual("I", obiekt.GetAtrybut("G5OZK"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RDZE"));
        }

        [TestMethod]
        public void test_budynek()
        {
            ObiektSwde obiekt = obiektKlasy("G5BUD");
            Assert.AreEqual("142401_2.0024.131/2.2_BUD", obiekt.GetAtrybut("G5IDB"));
            Assert.AreEqual("4200.00", obiekt.GetAtrybut("G5PEW"));
            Assert.IsNotNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPWŁ"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPWD"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RKRG"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RDOK"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RJDR"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RDZE"));
            //wkt
            GeometriaSwde geometria = obiekt.Geometria;
            string wkt = geometria.NaWkt();
            //POLYGON ((30 10, 10 20, 20 40, 40 40, 30 10))
            Assert.AreEqual("POLYGON ((7494127.98 5839106.28, 7494117.63 5839117.25, 7494060.92 5839173.76, 7494044.28 5839203.12, 7494037.13 5839219.37, 7494127.98 5839106.28))", wkt);
        }

        [TestMethod]
        public void test_lokal_samodzielny()
        {
            ObiektSwde obiekt = obiektKlasy("G5LKL");
            Assert.AreEqual("281401_4.0002.47/11.1_BUD.18_LOK", obiekt.GetAtrybut("G5IDL"));
            Assert.AreEqual("43.53", obiekt.GetAtrybut("G5PEW"));
            Assert.IsNull(obiekt.Geometria);
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RJDR"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RADR"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RPWŁ"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RDOK"));
            Assert.IsNotNull(obiekt.GetRelacjaLubNull("G5RBUD"));
        }

        private ObiektSwde obiektKlasy(string klasa)
        {
            IEnumerable<ObiektSwde> obiekty = _dokument.GetObiektyKlasy(klasa);
            Assert.AreEqual(1, obiekty.Count());
            ObiektSwde obiekt = obiekty.First();
            Assert.AreEqual(klasa, obiekt.Typ);
            return obiekt;
        }
    }
}
