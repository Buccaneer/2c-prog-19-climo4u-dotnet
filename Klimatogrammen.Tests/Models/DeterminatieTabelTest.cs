using System;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class DeterminatieTabelTest
    {
        private DeterminatieTabel _determinatieTabel;
        private Mock<Klimatogram> _klimatogramMock;

        [TestInitialize]
        public void Init()
        {
            _determinatieTabel = new DeterminatieTabelMock().MaakDeterminatieTabelEersteGraad();
            _klimatogramMock = new KlimatogramMockFactory().MaakKlimatogramMock();
        }

        [TestMethod]
        public void DeterminerenGeeftJuisteKlimaatTerug()
        {
            ResultaatBlad resultaat = _determinatieTabel.Determineer(_klimatogramMock.Object);
            string verwacht = "Gematigd en droog";
           
            Assert.AreEqual(verwacht, resultaat.KlimaatType);
        }

        [TestMethod]
        public void DeterminatieGeeftAlsResultaatJuist()
        {
            ResultaatBlad result = _determinatieTabel.Determineer(_klimatogramMock.Object);
            Resultaat resultaat = _determinatieTabel.ValideerGebruikerResultaat(result, _klimatogramMock.Object);
            Assert.AreEqual(resultaat, Resultaat.Juist);
        }


       
    }
}
