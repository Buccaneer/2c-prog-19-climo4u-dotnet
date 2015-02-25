using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Klimatogrammen.Infrastructure;
using System.Web.Mvc;
using Klimatogrammen.Controllers;
using Klimatogrammen.ViewModels;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Models.DAL;
using Moq;
using System.Linq;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    class OefeningVraagControllerTest
    {
        private OefeningVragenController _vraagController;
        private Klimatogram _mockKlimatogram;

        [TestInitialize]
        public void Init()
        {
            _vraagController = new OefeningVragenController();
            _mockKlimatogram = mockKlimatogramTrainen();
        }

        private Klimatogram mockKlimatogramTrainen()
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
            return mock.Object;
        }

        [TestMethod]
        public void IndienGeenKlimatogramRedirectNaarKlimatogram()
        {
            RedirectToRouteResult result = _vraagController.Index(new Leerling{Graad=Graad.Een},null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeeftVragenWeer()
        {
            ViewResult result = _vraagController.Index(new Leerling{Graad=Graad.Een, Klimatogram = _mockKlimatogram},VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram)) as ViewResult;
            VragenIndexViewModel vIVM = result.Model as VragenIndexViewModel;
            Assert.AreEqual(7, vIVM.VraagViewModels.Count);
            foreach (VraagViewModel v in vIVM.VraagViewModels)
            {
                Assert.IsNotNull(v.Antwoorden);
                Assert.IsNotNull(v.VraagTekst);
            }
        }

        [TestMethod]
        public void ValideertVragen()
        {
            VraagRepository vRep = VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram);
            VragenIndexViewModel vIVM = new VragenIndexViewModel(vRep);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels)
            {
                vVM.Antwoord = vVM.Antwoorden.First().Text;
            }
            _vraagController.Index(new Leerling{Graad=Graad.Een, Klimatogram=_mockKlimatogram}, vRep, vIVM);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels)
            {
                Assert.IsNotNull(vVM.ValidatieTekst);
            }
        }

        [TestMethod]
        public void AlleVragenZijnCorrect()
        {
            VraagRepository vRep = VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram);
            VragenIndexViewModel vIVM = new VragenIndexViewModel(vRep);
            string[] antwoorden = { "September", "30,1", "Februari", "2", "9", "87", "88"};
            for (int i = 0; i < 7; i++)
            {
                vIVM.VraagViewModels.ElementAt(i).Antwoord = antwoorden[i];
            }
            _vraagController.Index(new Leerling{Graad=Graad.Een, Klimatogram = _mockKlimatogram}, vRep, vIVM);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels)
            {
                Assert.IsTrue(vVM.Resultaat);
            }
        }
    }
}
