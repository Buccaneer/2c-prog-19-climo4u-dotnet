using System;
using Klimatogrammen.Tests.Mock;
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
    public class OefeningVraagControllerTest {
        private OefeningVragenController _vraagController;
        private Mock<Klimatogram> _mockKlimatogram;
        private Mock<Graad> _graadMock;
        private GraadMockFactory _graadMockFactory = new GraadMockFactory();

        [TestInitialize]
        public void Init() {
            _vraagController = new OefeningVragenController();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            _graadMock = _graadMockFactory.MaakEersteGraadAan();
        }

        [TestMethod]
        public void IndienGeenKlimatogramRedirectNaarKlimatogram() {
            RedirectToRouteResult result =
                _vraagController.Index(new Leerling {Graad = _graadMock.Object}, null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeeftVragenWeer() {
            ViewResult result =
                _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object},
                 _graadMock.Object.Vragen) as ViewResult;
            VragenIndexViewModel vIVM = result.Model as VragenIndexViewModel;
            Assert.AreEqual(7, vIVM.VraagViewModels.Count);
            foreach (VraagViewModel v in vIVM.VraagViewModels) {
                Assert.IsNotNull(v.Antwoorden);
                Assert.IsNotNull(v.VraagTekst);
            }
        }

        [TestMethod]
        public void ValideertVragen() {
            VragenIndexViewModel vIVM = new VragenIndexViewModel(_graadMock.Object.Vragen, _mockKlimatogram.Object);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels) {
                vVM.Antwoord = vVM.Antwoorden.First().Text;
            }
            _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object},
                _graadMock.Object.Vragen, vIVM);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels) {
                Assert.IsNotNull(vVM.ValidatieTekst);
            }
        }

        [TestMethod]
        public void AlleVragenZijnCorrect() {
            VragenIndexViewModel vIVM = new VragenIndexViewModel(_graadMock.Object.Vragen, _mockKlimatogram.Object);
            string[] antwoorden = {"September"};
            vIVM.VraagViewModels.ElementAt(0).Antwoord = "September";
            _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object},
                _graadMock.Object.Vragen, vIVM);
            foreach (VraagViewModel vVM in vIVM.VraagViewModels) {
                Assert.IsTrue(vVM.Resultaat);
            }
        }

        [TestMethod]
        public void EnkelLeerlingEersteGraadKanVragenMaken() {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            RedirectToRouteResult result =
                _vraagController.Index(new Leerling() {Graad = _graadMock.Object}, _graadMock.Object.Vragen) as
                    RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
            //RedirectToRouteResult result2 = _vraagController.Index(new Leerling() { Graad = Graad.Drie }, vRep) as RedirectToRouteResult;
            ////Assert.IsNotNull(result);
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("Home", result.RouteValues["controller"]); //zie opmerking

            // OPMERKING : 
            // Moet aangepast worden zodra Controllers beschikbaar zijn zodat Leerling 3de graad meteen naar Oefening gaat
        }

        [TestMethod]
        public void LeerlingNullRedirectNaarGraadSelectie(){
    

    //{
        //    VraagRepository vRep = VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram);
            RedirectToRouteResult result = _vraagController.Index(null, null) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void VragenRepositoryNullRedirectNaarKlimatogramSelectie()
        {
            RedirectToRouteResult result = _vraagController.Index(new Leerling() { Graad = _graadMock.Object }, null) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
            RedirectToRouteResult result2 = _vraagController.Index(new Leerling() { Graad = _graadMock.Object }, null) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result2.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result2.RouteValues["controller"]);
        }
    }
}
