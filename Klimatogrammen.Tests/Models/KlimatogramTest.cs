using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
    /// Deze testklasse bevat testen voor de klasse Klimatogram
    /// Volgende zaken worden getest:
    ///         * Klimatogram wordt aangemaakt
    ///         * Gemiddelde temp met minder dan 12 cijfers
    ///         * Gemiddelde temp kleiner dan -273.15
    ///         * Gemiddelde temp groter dan 500
    ///         * Gemiddelde neerslag met minder dan 12 cijfers
    ///         * Gemiddelde neerslag kleiner dan nul
    ///         * Totale neerslag wordt berekend met sum
    ///         * Totale gemiddelde temp wordt berekend met average
    /// </summary>
    [TestClass]
    public class KlimatogramTest
    {

       [TestMethod]
        public void KlimatogramHeeftGemiddeldeTempEnGemiddeldeNeerslag()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);

           Assert.AreEqual(k.GemiddeldeNeerslag, gemiddeldeNeer);
           Assert.AreEqual(k.GemiddeldeTemperatuur, gemiddeldeTemp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GemiddeldeTempMetMinderDanTwaalfCijfersGooitExceptie()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GemiddeldeTempMoetGroterZijnDanMin273Punt15OfGooitExceptie()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, -274, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GemiddeldeTempMoetKleinerZijnDan500OfGooitExceptie()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, 501, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GemiddeldeNeerslagMetMinderDanTwaalfCijfersGooitExceptie()
        {
            int[] gemiddeldeNeer = { 12 };
            double[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GemiddeldeNeerslagMoetGroterZijnDanNulOfGooitExceptie()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, -15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
        }

        [TestMethod]
        public void TotaleNeerslagWordtBerekendMetSum()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
            Assert.AreEqual(k.TotaalNeerslag, 210);
        }

        [TestMethod]
        public void TotaalGemiddeldeTemperatuurWordtBerekendMetAverage()
        {
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18 };
            double[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            Klimatogram k = new Klimatogram(gemiddeldeTemp, gemiddeldeNeer);
            Assert.AreEqual(k.TotaalGemiddeldeTemperatuur, 14.72);
        }
    }

}
