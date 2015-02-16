using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class NeerslagTest
    {
        [TestMethod]
        public void NeerslagHeeftEenWaarde()
        {
            int waarde = 10;
            Neerslag neerslag = new Neerslag(waarde);
            Assert.AreEqual(waarde, neerslag.Waarde);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NeerslagMoetGroterZijnDanNulOfGooitExceptie()
        {
            Neerslag neerslag = new Neerslag(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TempMoetKleinerZijnDan500OfGooitExceptie()
        {
            Temperatuur temp = new Temperatuur(500.10);
        }
    }
}
