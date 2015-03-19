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
    public class OefeningVraagControllerTest 
    {
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
        public void GeenKlimatogramRedirectNaarKlimatogramController() {
            RedirectToRouteResult result =
                _vraagController.Index(new Leerling {Graad = _graadMock.Object}) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeeftVragenWeer() {
            ViewResult result =
                _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object}) as ViewResult;
           VragenIndexViewModel vIVM = result.Model as VragenIndexViewModel;
            Assert.AreEqual(_graadMock.Object.Vragen.Count, vIVM.Vragen.Count);
            foreach (VraagViewModel v in vIVM.Vragen) {
                Assert.IsNotNull(v.Antwoorden);
                Assert.IsNotNull(v.VraagTekst);
            }
        }

        [TestMethod]
        public void ValideertVragenFoutief() {
            AntwoordViewModel aVM = new AntwoordViewModel(new string[] {"Juli"});
            
            ViewResult result = _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object},
                aVM) as ViewResult;

            VragenIndexViewModel vivm = result.Model as VragenIndexViewModel;

            Assert.IsNotNull(vivm.Vragen.ElementAt(0).Resultaat);
            Assert.IsFalse(vivm.Vragen.ElementAt(0).Resultaat.Value);
        }

        [TestMethod]
        public void AlleVragenZijnCorrect() {
            string[] antwoorden = { "September" };
            AntwoordViewModel avm = new AntwoordViewModel(antwoorden);
           


            ViewResult result = _vraagController.Index(new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object},
                avm)as ViewResult;

            VragenIndexViewModel vIVM = result.Model as VragenIndexViewModel;
            foreach (VraagViewModel vVM in vIVM.Vragen) {
                Assert.IsNotNull(vVM.Resultaat);
                Assert.IsTrue(vVM.Resultaat.Value);
            }
        }

        [TestMethod]
        public void EnkelLeerlingEersteGraadKanVragenMaken() {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            RedirectToRouteResult result =
                _vraagController.Index(new Leerling() {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object}) as
                    RedirectToRouteResult;
            
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Determinatie", result.RouteValues["controller"]);
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            RedirectToRouteResult result2 = _vraagController.Index(new Leerling() { Graad= _graadMock.Object,Klimatogram = _mockKlimatogram.Object}) as RedirectToRouteResult;
            
            Assert.AreEqual("Index", result2.RouteValues["action"]);
            Assert.AreEqual("LocatieOefening", result2.RouteValues["controller"]);
        }

        [TestMethod]
       public void GeenLeerlingRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _vraagController.Index(null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeenGraadRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _vraagController.Index(new Leerling { Graad = null, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }
        

        [TestMethod]
        public void GeenVragenRedirectNaarDeterminatieSelectieVoorGraadTwee() {
            _graadMock = _graadMockFactory.MaakTweedeGraadTweedeJaarAan();
            RedirectToRouteResult result = _vraagController.Index(new Leerling() { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object}) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Determinatie", result.RouteValues["controller"]);

            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            RedirectToRouteResult result2 = _vraagController.Index(new Leerling() { Graad = _graadMock.Object , Klimatogram = _mockKlimatogram.Object}) as RedirectToRouteResult;
            Assert.AreEqual("Index", result2.RouteValues["action"]);
            Assert.AreEqual("LocatieOefening", result2.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeenVragenRedirectNaarDeterminatieSelectieVoorGraadDrie()
        {
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            RedirectToRouteResult result2 = _vraagController.Index(new Leerling() { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result2.RouteValues["action"]);
            Assert.AreEqual("LocatieOefening", result2.RouteValues["controller"]);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetJsonGeeftEnkelJsonTerugAlsErEenLeerlingBestaatMetKlimatogramIn() {
            Leerling l = new Leerling() {Graad = _graadMock.Object};
            JsonResult result = _vraagController.GetJSON(l) as JsonResult;
        
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetJsonGooitFoutmeldingBijAanroepenZonderleerling() {
            JsonResult result = _vraagController.GetJSON(null) as JsonResult;

        }
    }
}
