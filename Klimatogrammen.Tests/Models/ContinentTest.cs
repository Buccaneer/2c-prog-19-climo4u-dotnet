using System;
using System.Collections;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
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
