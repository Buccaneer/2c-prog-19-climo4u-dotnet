using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Generator;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
    /// Deze testklasse bevat testen voor de klasse Klimatogram
    /// Volgende zaken worden getest:
    ///         * Klimatogram geeft de gemiddelde temperaturen terug
    ///         * Klimatogram geeft de neerslagen terug
    ///         * Klimatogram geeft de totale neerslag per jaar terug
    ///         * Klimatogram geeft de gemiddelde temperatuur over het jaar terug
    /// </summary>
    [TestClass]
    public class KlimatogramTest
    {
        private MaandMockFactory _maandMockFactory;

        [TestInitialize]
        public void init()
        {
            _maandMockFactory = new MaandMockFactory();
        }

        [TestMethod]
        public void KlimatogramGeeftGemiddeldeTemperatuur()
        {
            int[] neerslagen = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            double[] temperaturen = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            var maanden = _maandMockFactory.MaakMaandenInJaarMock(neerslagen, temperaturen);
            Klimatogram k = new Klimatogram(maanden.Select(m => m.Object).ToList());
            CollectionAssert.AreEqual(k.GeefTemperaturen().ToList(), temperaturen.ToList());
        }

        [TestMethod]
        public void KlimatogramGeeftGemiddeldeNeerslag()
        {
            int[] neerslagen = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            double[] temperaturen = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            var maanden = _maandMockFactory.MaakMaandenInJaarMock(neerslagen, temperaturen);
            Klimatogram k = new Klimatogram(maanden.Select(m => m.Object).ToList());
            CollectionAssert.AreEqual(k.GeefNeerslagen().ToList(), neerslagen.ToList());
        }

        [TestMethod]
        public void KlimatogramBerekentTotaleJaarNeerslag()
        {
            int[] neerslagen = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            double[] temperaturen = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            var maanden = _maandMockFactory.MaakMaandenInJaarMock(neerslagen, temperaturen);
            Klimatogram k = new Klimatogram(maanden.Select(m => m.Object).ToList());
            Assert.AreEqual(neerslagen.Sum(), k.GeefTotaleNeerslag());
        }

        [TestMethod]
        public void KlimatogramBerekentGemiddeldeJaarTemperatuur()
        {
            int[] neerslagen = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            double[] temperaturen = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            var maanden = _maandMockFactory.MaakMaandenInJaarMock(neerslagen, temperaturen);
            Klimatogram k = new Klimatogram(maanden.Select(m => m.Object).ToList());
            //double gemiddelde = Math.Round(temperaturen.Average(), 1);
            double gemiddelde = temperaturen.Average();
            Assert.AreEqual(gemiddelde, k.GeefGemiddeldeTemperatuur());
        }
    }

}
