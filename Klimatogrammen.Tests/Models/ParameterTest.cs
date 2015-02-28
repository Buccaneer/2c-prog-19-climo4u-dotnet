using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Klimatogrammen.Tests.Models {
    [TestClass]
    public class ParameterTest {
        private Mock<Klimatogram> _mockKlimatogram;
        private ICollection _maanden;

        [TestInitialize]
        public void Init() {
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            var maanden = new List<string>();
            maanden.Add("Januari");
            maanden.Add("Februari");
            maanden.Add("Maart");
            maanden.Add("April");
            maanden.Add("Mei");
            maanden.Add("Juni");
            maanden.Add("Juli");
            maanden.Add("Augustus");
            maanden.Add("September");
            maanden.Add("Oktober");
            maanden.Add("November");
            maanden.Add("December");
            _maanden = maanden.ToList();
        }

        #region "Constante parameter testen"

        [TestMethod]
        public void ConstanteParameterGeeftWaardeCorrectTerug() {
            double waarde = 4.5;
            ConstanteParameter cp = new ConstanteParameter(waarde);

            Assert.AreEqual(waarde, cp.BerekenWaarde(_mockKlimatogram.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConstanteParameterGeeftExceptionBijAanvragenMogelijkeAntwoorden() {
            double waarde = 4.5;
            ConstanteParameter cp = new ConstanteParameter(waarde);

            cp.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConstanteParameterGeeftExceptionBijAanvragenBeschrijving() {
            double waarde = 4.5;
            ConstanteParameter cp = new ConstanteParameter(waarde);

            cp.GeefBeschrijving();
        }

        #endregion

        #region WarmsteMaandTesten

        [TestMethod]
        public void ParameterWarmsteMaandGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterWarmsteMaand();

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);

            CollectionAssert.AreEquivalent(_maanden, mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void ParameterWarmsteMaandGeeftJuistAntwoordTerug() {
            Parameter p = new ParameterWarmsteMaand();
            var verwacht =
                _mockKlimatogram.Object.Maanden.First(
                    m => m.Temperatuur.Equals(_mockKlimatogram.Object.Maanden.Max(t => t.Temperatuur))).Naam;
            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region TemperatuurWarmsteMaandTesten

        [TestMethod]
        public void ParameterTemperatuurWarmsteMaandGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterTemperatuurWarmsteMaand();

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
            ICollection<string> verwacht = _mockKlimatogram.Object.Maanden.Select(m => m.Temperatuur.ToString()).ToList();

            CollectionAssert.AreEquivalent(verwacht.ToList(), mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void ParameterTemperatuurWarmsteMaandBerekentCorrecteWaarde() {
            Parameter p = new ParameterTemperatuurWarmsteMaand();
            double verwacht = _mockKlimatogram.Object.Maanden.Max(m => m.Temperatuur);

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }

        #endregion

        #region KoudsteMaandTesten

        [TestMethod]
        public void ParameterKoudsteMaandGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterKoudsteMaand();

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);

            CollectionAssert.AreEquivalent(_maanden, mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void ParameterKoudsteMaandGeeftJuistAntwoordTerug() {
            Parameter p = new ParameterKoudsteMaand();
            var verwacht =
                _mockKlimatogram.Object.Maanden.First(
                    m => m.Temperatuur.Equals(_mockKlimatogram.Object.Maanden.Min(t => t.Temperatuur))).Naam;
            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region "Temperatuur koudste maanden testen"
        [TestMethod]
        public void ParameterTemperatuurKoudsteMaandGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterTemperatuurKoudsteMaand();

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
            ICollection<string> verwacht = _mockKlimatogram.Object.Maanden.Select(m => m.Temperatuur.ToString()).ToList();

            CollectionAssert.AreEquivalent(verwacht.ToList(), mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void ParameterTemperatuurKoudsteMaandBerekentCorrecteWaarde() {
            Parameter p = new ParameterTemperatuurKoudsteMaand();
            double verwacht = _mockKlimatogram.Object.GeefTemperaturen().Min();

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region "Aantal droge maanden testen"

        [TestMethod]
        public void ParamaterAantalDrogeMaandenGeeftalleMogelijkeAntwoorden() {
            Parameter p = new ParameterAantalDrogeMaanden();

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
            ICollection<string> verwacht = Enumerable.Range(1, 12).Select(i => i.ToString()).ToList();

            CollectionAssert.AreEquivalent(verwacht.ToList(), mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void ParameterAantalDrogeMaandenBerekentCorrecteWaarde() {
            Parameter p = new ParameterAantalDrogeMaanden();
            int verwacht = _mockKlimatogram.Object.Maanden.Count(m => m.Neerslag/2.0 <= m.Temperatuur);

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }

        #endregion

        #region "Neerslag in de zomer testen"

        [TestMethod]
        public void ParameterNeerslagInZomerGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterNeerslagZomer();

            int zomer = 87;
            int winter = 88;

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);

            CollectionAssert.Contains(mogelijkeAntwoorden.ToList(), zomer.ToString());
            CollectionAssert.Contains(mogelijkeAntwoorden.ToList(), winter.ToString());
        }

        [TestMethod]
        public void ParameterNeerslagInZomerBerekentCorrecteWaarde() {
            Parameter p = new ParameterNeerslagZomer();
            int verwacht = 87;

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region "Neerslag in de winter testen"

        [TestMethod]
        public void ParameterNeerslagInWinterGeeftAlleMogelijkeAntwoorden() {
            Parameter p = new ParameterNeerslagWinter();
            int zomer = 87;
            int winter = 88;

            ICollection<string> mogelijkeAntwoorden = p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);

            CollectionAssert.Contains(mogelijkeAntwoorden.ToList(), zomer.ToString());
            CollectionAssert.Contains(mogelijkeAntwoorden.ToList(), winter.ToString());
        }

        [TestMethod]
        public void ParameterNeerslagInWinterBerekentCorrecteWaarde() {
            Parameter p = new ParameterNeerslagWinter();
            int verwacht = 88;

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region "Gemiddelde jaar temperatuur"

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ParameterGemiddeldeJaarTemperatuurGeeftExceptionBijAanroepenMogelijkeAntwoordenTerug() {
            Parameter p = new ParameterGemiddeldeTemperatuurJaar();

            p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
        }

        [TestMethod]
        public void ParameterGemiddeldeJaarTemperatuurGeeftCorrecteWaarde() {
            Parameter p = new ParameterGemiddeldeTemperatuurJaar();
            double verwacht = _mockKlimatogram.Object.GeefGemiddeldeTemperatuur();
            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion

        #region "Totale jaar neerslag"
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ParameterTotaleJaarNeerslagGeeftExceptionBijAanroepenMogelijkeAntwoordenTerug() {
            Parameter p = new ParameterTotaleNeerslagJaar();

            p.GeefMogelijkeAntwoorden(_mockKlimatogram.Object);
        }

        [TestMethod]
        public void ParameterTotaleJaarNeerslagBerekentCorrecteWaarde() {
            Parameter p = new ParameterTotaleNeerslagJaar();
            int verwacht = _mockKlimatogram.Object.GeefTotaleNeerslag();

            Assert.AreEqual(verwacht, p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion
    }
}