using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Klimatogrammen.Controllers;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.DAL;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Klimatogrammen.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _homeController;
        private ISessionRepository _sessionRepository;
        private IGraadRepository _graadRepository;
        private GraadMockFactory _graadMockFactory;
        private Mock<Graad> _graadMock;

        [TestInitialize]
        public void Init()
        {
            _sessionRepository= new SessionRepositoryMock();
            _graadRepository = new GraadRepositoryMock();
            _graadMockFactory = new GraadMockFactory();
            _homeController = new HomeController(_sessionRepository, _graadRepository);
        }

        [TestMethod]
        public void IndexIsNietNull()
        {
            ViewResult result = _homeController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexHttpPostRedirectNaarAndereView()
        {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling{ Graad = _graadMock.Object};
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            RedirectToRouteResult result = _homeController.Index(leerlingIVM) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void LeerlingZitInSessieNaCorrectePost()
        {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            _homeController.Index(leerlingIVM);
            Assert.IsNotNull(_sessionRepository["leerling"]);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void IndexGeeftViewTerugBijGraadTweeJaarNul() //overbodige test?
        {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            _graadMock.Setup(m => m.Jaar).Returns(0);
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            _homeController.Index(leerlingIVM);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void IndexGeeftViewTerugBijGraadEenMetJaarEen() //overbodige test?
        {
            _graadMock = _graadMockFactory.MaakEersteGraadAan();
            _graadMock.Setup(m => m.Jaar).Returns(1);
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            _homeController.Index(leerlingIVM);
        }
    }
}
