using System;
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

        [TestInitialize]
        public void Init()
        {
            _locatieOefeningController = new LocatieOefeningController();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
        }

        [TestMethod]
        public void GeenLeerlingRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GeenGraadRedirectNaarHomeController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GraadEenMetKlimatogramRedirectNaarOefeningVragenController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("OefeningVragen", result.RouteValues["controller"]);
        }

       [TestMethod]
        public void GraadTweeMetKlimatogramRedirectNaarDeterminatieController()
        {
            RedirectToRouteResult result =
                _locatieOefeningController.Index(new Leerling { Graad = _graadMock.Object }) as RedirectToRouteResult;
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
        public void VerbeterVegetatieVragenGeeftCorrecteViewTerug()
        {
            Leerling leerling = new Leerling(){Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object};
            AntwoordViewModel antwoord = new AntwoordViewModel();
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
