using System;
using System.Collections;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
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
    public class ContintentTest
    {
        [TestMethod]
        public void ContinentHeeftEenNaam()
        {
            Continent c = new Continent() {Naam = "Afrika"};
            Assert.AreEqual(c.Naam, "Afrika");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamContinentAlsNullGeeftFoutmelding()
        {
            Continent c = new Continent() { Naam = null };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamContinentKanGeenSpecialeTekensBevatten()
        {
            Continent c = new Continent() { Naam = "€;%µ£$@#<>+*" };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaamContinentKanNietNietsBevatten()
        {
            Continent c = new Continent() { Naam = "" };
        }

        [TestMethod]
        public void LandenZijnInDeCollectionAanwezig()
        {
           Assert.Inconclusive();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
         public void NullAlsLandenGeeftFoutmelding()
        {
            Continent c = new Continent() { Naam = "Afrika" };
            ICollection<Land> landen = null;
        }

    }
}
