using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class TemperatuurTest
    {
        [TestMethod]
        public void TemperatuurHeeftEenWaarde()
        {
            double waarde = 10.0;
            Temperatuur temp = new Temperatuur(waarde);
            Assert.AreEqual(waarde, temp.Waarde);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TempMoetGroterZijnDanMin273Punt15OfGooitExceptie()
        {
            Temperatuur temp = new Temperatuur(-274.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TempMoetKleinerZijnDan500OfGooitExceptie()
        {
            Temperatuur temp = new Temperatuur(500.10);
        }
    }
}
