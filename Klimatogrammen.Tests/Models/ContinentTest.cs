using System;
using System.Collections;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
    /// OPMERKING : TESTKLASSE IS EIGENLIJK NIET NODIG WANT DE CONTINENTEN KOMEN UIT DE DATABASE
    /// Deze testklasse bevat testen voor de klasse Continent
    /// Volgende zaken worden getest:
    ///         * Correcte naam
    ///         * Naam null
    ///         * Naam met rare tekens
    ///         * Geen naam
    ///         * Opgevulde lijst van landen
    ///         * Lijst van landen is null
    /// </summary>
    [TestClass]
    public class ContinentTest
    {
        [TestMethod]
        public void ContinentHeeftEenNaam()
        {
            Continent c = new Continent("Afrika");
            Assert.AreEqual(c.Naam, "Afrika");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NaamContinentAlsNullGeeftFoutmelding()
        {
            Continent c = new Continent(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamContinentKanGeenSpecialeTekensBevatten()
        {
            Continent c = new Continent("€;%µ£$@#<>+*");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamContinentKanNietNietsBevatten()
        {
            Continent c = new Continent("");
        }

        [TestMethod]
        public void LandenZijnInDeCollectionAanwezig()
        {
            Continent c = new Continent("Afrika");
            List<Land> landen = new List<Land>();
            landen.Add(new Land("Zimbabwe"));
            c.Landen = landen;
            Assert.AreEqual(landen, c.Landen);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
         public void NullAlsLandenGeeftFoutmelding()
        {
            Continent c = new Continent("Afrika");
            c.Landen = null;
        }

    }
}
