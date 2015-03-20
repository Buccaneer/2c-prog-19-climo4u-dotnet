using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Klimatogrammen.Controllers;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Klimatogrammen.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    public class LocatieOefeningControllerTest
    {

        private LocatieOefeningController _locatieOefeningController;
        private Mock<Klimatogram> _mockKlimatogram;
        private Mock<Graad> _graadMock;
        private GraadMockFactory _graadMockFactory = new GraadMockFactory();
        private LeerlingMock _leerlingMock = new LeerlingMock();
        private Mock<Leerling> _leerling;

        [TestInitialize]
        public void Init()
        {
            _locatieOefeningController = new LocatieOefeningController();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            _leerling = _leerlingMock.maakJuisteLeerling();

        }

        [TestMethod]
        public void GeenLeerlingRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeenGraadRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = null, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GraadEenMetKlimatogramRedirectNaarOefeningVragenController()
        {
            _graadMock = _graadMockFactory.MaakEersteGraadAan();
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("OefeningVragen", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GraadTweeMetKlimatogramRedirectNaarDeterminatieController()
        {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Determinatie", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void IndexLocatieOefeningControllerIsNietNull()
        {
            ViewResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerbeterVegetatieVragenGeeftVoorCorrectAntwoordTrue()
        {
            Leerling leerling = _leerling.Object;
            Klimatogram k = _mockKlimatogram.Object;
            AntwoordViewModel antwoorden = new AntwoordViewModel() { Antwoord = new[] { leerling.Graad.DeterminatieTabel.Determineer(k).VegetatieType.Naam } };
            ViewResult result = _locatieOefeningController.VerbeterVegetatieVragen(_leerling.Object, antwoorden) as ViewResult;

            OefeningLocatieVegTypesIndexViewModel vm = result.Model as OefeningLocatieVegTypesIndexViewModel;

            Assert.IsTrue(vm.AllesJuist.Value);
        }

        [TestMethod]
        public void VerbeterVegetatieVragenGeeftVoorFoutiefAntwoordGeeftFalse()
        {
            Leerling leerling = _leerling.Object;
            Klimatogram k = _mockKlimatogram.Object;
            AntwoordViewModel antwoorden = new AntwoordViewModel() { Antwoord = new[] { "blabla" } };
            ViewResult result = _locatieOefeningController.VerbeterVegetatieVragen(_leerling.Object, antwoorden) as ViewResult;

            OefeningLocatieVegTypesIndexViewModel vm = result.Model as OefeningLocatieVegTypesIndexViewModel;

            Assert.IsFalse(vm.AllesJuist.Value);
        }

        [TestMethod]
        public void VerbeterVegetatieVragenGeeftEenViewWeer()
        {
            Leerling leerling = _leerling.Object;
            Klimatogram k = _mockKlimatogram.Object;
            AntwoordViewModel antwoorden = new AntwoordViewModel() { Antwoord = new[] { "blabla" } };
            ViewResult result = _locatieOefeningController.VerbeterVegetatieVragen(_leerling.Object, antwoorden) as ViewResult;

            OefeningLocatieVegTypesIndexViewModel vm = result.Model as OefeningLocatieVegTypesIndexViewModel;

            Assert.IsNotNull(vm);
        }


        [TestMethod]
        public void HttpPostIndexGeeftBijFoutenDeEersteViewWeer()
        {
            _leerling = _leerlingMock.maakFouteLeerling();
            Leerling leerling = _leerling.Object;

            ViewResult result = _locatieOefeningController.Index(leerling, new[] { "blabla" }, new[] { "0" }) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HttpPostIndexGeeftBijAllesGoedEenOefeningLocatieViewWeer()
        {
            Leerling leerling = _leerling.Object;

            ViewResult result = _locatieOefeningController.Index(leerling, new[] { "Mock Klimatogram" },new[] { "0" }) as ViewResult;

            OefeningLocatieVegTypesIndexViewModel vm = result.Model as OefeningLocatieVegTypesIndexViewModel;

            Assert.IsNotNull(vm);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GeefFoutieveKlimatogrammenGooitFoutmeldingBijAanroepenZonderleerling()
        {
            JsonResult result = _locatieOefeningController.GeefFoutieveKlimatogrammen(null) as JsonResult;

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GeefKlimatogrammenGooitFoutmeldingBijAanroepenZonderleerling()
        {
            JsonResult result = _locatieOefeningController.GeefFoutieveKlimatogrammen(null) as JsonResult;
        }
    }
}

