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
    public class GraadControllerTest
    {
        private GraadController _homeController;
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
            _homeController = new GraadController(_sessionRepository, _graadRepository);
        }

        [TestMethod]
        public void IndexIsNietNull()
        {
            ViewResult result = _homeController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HttpPostIndexRedirectNaarAndereView()
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
    }
}
