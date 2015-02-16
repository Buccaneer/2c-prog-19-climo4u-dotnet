using System;
using System.Text;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
    /// Deze testklasse bevat testen voor de klasse Land
    /// Volgende zaken worden getest:
    ///         * Correcte naam
    ///         * Naam null
    ///         * Naam met rare tekens
    ///         * Geen naam
    ///         * Opgevulde lijst van klimatogrammen
    ///         * Lijst van klimatogrammen is null
    /// </summary>
    [TestClass]
    public class LandTest
    {
        [TestMethod]
        public void LandHeeftEenNaam()
        {
            Land l = new Land("Congo");
            Assert.AreEqual(l.Naam, "Congo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NaamLandAlsNullGeeftFoutmelding()
        {
            Land l = new Land(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamLandKanGeenSpecialeTekensBevatten()
        {
            Land l = new Land("€;%µ£$@#<>+*");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamLandKanNietNietsBevatten()
        {
            Land l = new Land("");
        }

        [TestMethod]
        public void KlimatogrammenZijnInDeCollectionAanwezig()
        {
            Land l = new Land("Zimbabwe");
            List<Klimatogram> klimatogrammen = new List<Klimatogram>();
            klimatogrammen.Add(new Klimatogram());
            l.Klimatogrammen = klimatogrammen;
            Assert.AreEqual(klimatogrammen, l.Klimatogrammen);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullAlsKlimatogrammenGeeftFoutmelding()
        {
            Land l = new Land("Congo");
            l.Klimatogrammen = null;
        }

    }
}

