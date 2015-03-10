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
            Resultaat resultaat = _determinatieTabel.ValideerGebruikerResultaat(result);
            Assert.AreEqual(resultaat, Resultaat.Juist);
        }


        //[TestMethod]
        //public void DeterminatieGebruikerIsFoutief()
        //{
        //    _determinatieTabel.Determineer(_klimatogramMock.Object);
        //    Resultaat verwacht = Resultaat.Fout;
        //    while (!_determinatieTabel.HeeftLeerlingEindeBereikt())
        //        _determinatieTabel.NeemNeeKnoop();

        //    Assert.AreEqual(verwacht, _determinatieTabel.ValideerGebruiker());
        //}

        //[TestMethod]
        //public void DeterminatieGebruikerIsJuist()
        //{
        //    _determinatieTabel.Determineer(_klimatogramMock.Object);
        //    Resultaat verwacht = Resultaat.Juist;
        //    _determinatieTabel.NeemNeeKnoop();
        //    _determinatieTabel.NeemNeeKnoop();
        //    _determinatieTabel.NeemJaKnoop();
        //    _determinatieTabel.NeemNeeKnoop();

        //    Assert.AreEqual(verwacht, _determinatieTabel.ValideerGebruiker());
        //}

        //[TestMethod]
        //public void FoutieveDeterminatieGebruikerWistZijnPad()
        //{
        //    _determinatieTabel.Determineer(_klimatogramMock.Object);
        //    var huidige = _determinatieTabel.GeefDeterminatieGebruiker();
        //    _determinatieTabel.NeemJaKnoop();
        //    _determinatieTabel.ValideerGebruiker();
        //    var nu = _determinatieTabel.GeefDeterminatieGebruiker();

        //    Assert.AreEqual(huidige,nu);
        //}
    }
}
