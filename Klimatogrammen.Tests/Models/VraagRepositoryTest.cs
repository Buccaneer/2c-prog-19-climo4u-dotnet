using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class VraagRepositoryTest
    {

        Mock<Klimatogram> mockKlimatogram;

        [TestInitialize]
        public void Init()
        {
            mockKlimatogram = mockKlimatogramTrainen();
        }

        private Mock<Klimatogram> mockKlimatogramTrainen()
        {
            var mock = new Mock<Klimatogram>();
            mock.Setup(k => k.BeginJaar).Returns(1980);
            mock.Setup(k => k.EindJaar).Returns(2010);
            Neerslag[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 18 };
            Temperatuur[] gemiddeldeTemp = { 5.1, 2.0, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            mock.Setup(k => k.GemiddeldeNeerslag).Returns(gemiddeldeNeer);
            mock.Setup(k => k.GemiddeldeTemperatuur).Returns(gemiddeldeTemp);
            mock.Setup(k => k.Locatie).Returns("Mock Klimatogram");
            mock.Setup(k => k.TotaalGemiddeldeTemperatuur).Returns(5.1 + 2.2 + 10.5 + 12.7 + 18 + 20.4 + 21.2 + 25.2 + 30.1 + 19 + 10 + 2.2 / 12);
            mock.Setup(k => k.TotaalNeerslag).Returns(174);
            return mock;
        }

        [TestMethod]
        public void CreerVraagRepositoryTest()
        {
            VraagRepository vr = VraagRepository.CreerVragenVoorKlimatogram(mockKlimatogram.Object);
            //Assert.AreEqual(vr.Vragen.Count, 7);
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(0), typeof(VraagWarmsteMaand));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(1), typeof(VraagTemperatuurWarmsteMaand));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(2), typeof(VraagKoudsteMaand));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(3), typeof(VraagTemperatuurKoudsteMaand));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(4), typeof(VraagAantalDrogeMaanden));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(5), typeof(VraagNeerslagInDeZomer));
            Assert.IsInstanceOfType(vr.Vragen.ElementAt(6), typeof(VraagNeerslagInDeWinter));
            //Assert.AreEqual(vr.FoutieveVragen, 0);
            //Assert.AreEqual(vr.JuisteVragen, 0);
        }

        [TestMethod]
        public void ValideerVraagJuist()
        {
            VraagRepository vr = VraagRepository.CreerVragenVoorKlimatogram(mockKlimatogram.Object);
            vr.ValideerVraag(0, "September");
            Assert.AreEqual(1, vr.JuisteVragen.Count());
        }

        [TestMethod]
        public void ValideerVraagFout()
        {
            VraagRepository vr = VraagRepository.CreerVragenVoorKlimatogram(mockKlimatogram.Object);
            vr.ValideerVraag(0, "Oktober");
            Assert.AreEqual(1, vr.FoutieveVragen.Count());
        }

    }
}
