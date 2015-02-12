

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Controllers;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController homeController;

        [TestInitialize]
        public void Init()
        {
            homeController = new HomeController();
        }

        [TestMethod]
        public void IndexIsNietNull()
        {
            // Arrange

            // Act
            ViewResult result = homeController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexGeeftLeerlingIndexViewModelDoorAanView()
        {
            ViewResult result = homeController.Index() as ViewResult;
            LeerlingIndexViewModel leerlingIVM = result.Model as LeerlingIndexViewModel;
            SelectListItem[] list = (leerlingIVM.Graden).ToArray();

            Assert.AreEqual(3, list.Length);
            Assert.AreEqual("0", list[0].Value);
            Assert.AreEqual("Eerste", list[0].Text);
            Assert.AreEqual("1", list[1].Value);
            Assert.AreEqual("Tweede", list[1].Text);
            Assert.AreEqual("2", list[2].Value);
            Assert.AreEqual("Derde", list[2].Text);
        }

        [TestMethod]
        public void IndexHttpPostRedirectNaarAndereView()
        {
            Leerling leerling = new Leerling{ Graad = Graad.Twee, Jaar = 1 };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            RedirectToRouteResult result = homeController.Index(leerlingIVM) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["RedirectToAction"]);
        }

        [TestMethod]
        public void LeerlingZitInSessieNaCorrectePost()
        {
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 1 };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            homeController.Index(leerlingIVM);
            Assert.IsNotNull(homeController.HttpContext.Session["leerling"]);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void IndexGeeftViewTerugBijGraadTweeJaarNul()
        {
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 0 };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            homeController.Index(leerlingIVM);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void IndexGeeftViewTerugBijGraadEenMetJaarEen()
        {
            Leerling leerling = new Leerling { Graad = Graad.Een, Jaar = 1 };
            LeerlingIndexViewModel leerlingIVM = new LeerlingIndexViewModel(leerling);
            homeController.Index(leerlingIVM);
        }
    }
}
