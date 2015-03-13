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
using Klimatogrammen.Tests.Mock;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    public class KlimatogramControllerTest
    {

        private KlimatogramController _klimatogramController;
        private GraadMockFactory _graadMockFactory;
        private Mock<Graad> _graadMock;

        [TestInitialize]
        public void Init()
        {
            _klimatogramController = new KlimatogramController();
            _graadMockFactory = new GraadMockFactory();
        }

        /// <summary>
        ///  controleer indien leerling niet in sessie dat redirect naar jaar/graad
        /// </summary>

        [TestMethod]
        public void IndienGeenLeerlingInSessieRedirectNaarGraad()
        {
            RedirectToRouteResult result = _klimatogramController.Index(null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        /// <summary>
        /// controleer geeft lijst continenten weer
        /// </summary>
        [TestMethod]
        public void GeeftLijstContinentenWeer()
        {
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            ViewResult result = _klimatogramController.Index(leerling) as ViewResult;
            KlimatogramKiezenIndexViewModel kkIVM = result.Model as KlimatogramKiezenIndexViewModel;
            CollectionAssert.AreEqual(_graadMock.Object.Continenten.Select(c => c.Naam).ToList(), kkIVM.Werelddelen.Select(c => c.Text).ToList());
        }

        /// <summary>
        /// controleer geeft correcte lijst met landen voor geselecteerd continent
        /// </summary>

        [TestMethod]
        public void GeeftLandenVoorGeselecteerdContinentWeer()
        {
            string europa = "Europa";
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            var vmContinent = new KlimatogramKiezenIndexViewModel();
            vmContinent.Werelddeel = europa;
            var result = _klimatogramController.Index(leerling, vmContinent) as PartialViewResult;
            var vmLand = result.Model as KlimatogramKiezenLandViewModel;
            CollectionAssert.AreEqual(_graadMock.Object.Continenten.First(c => c.Naam.Equals(europa)).Landen.Select(l => l.Naam).ToList(), vmLand.Landen.Select(l => l.Text).ToList());
        }


        /// <summary>
        /// controleer geeft correcte lijst met locaties (klimatogrammen) voor geselecteerd land
        ///  </summary>
        [TestMethod]
        public void GeeftLocatiesVoorGeselecteerdLandWeer()
        {
            string belgie = "België";
            string europa = "Europa";
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            var vmLand = new KlimatogramKiezenLandViewModel();
            vmLand.Land = belgie;
            var continent = _graadMock.Object.Continenten.First(c => c.Naam.Equals(europa));
            var result = _klimatogramController.KiesLand(leerling, continent, vmLand) as PartialViewResult;
            var vmLocatie = result.Model as KlimatogramKiezenLocatieViewModel;
            CollectionAssert.AreEqual(continent.Landen.First(l => l.Naam.Equals(belgie)).Klimatogrammen.Select(k => k.Locatie).ToList(), vmLocatie.Locaties.Select(l => l.Text).ToList());
        }

        /// <summary>
        ///  controleer leerling eerste graad krijgt enkel europa als continent
        /// </summary>

        [TestMethod]
        public void LeerlingEersteGraadKanEnkelEuropaAlsContinentKiezen()
        {
            _graadMock = _graadMockFactory.MaakEersteGraadAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            var result = _klimatogramController.Index(leerling) as ViewResult;
            var kkIVM = result.Model as KlimatogramKiezenIndexViewModel;
            Assert.AreEqual(1, kkIVM.Werelddelen.Count());
            Assert.AreEqual("Europa", kkIVM.Werelddelen.First().Text);
        }

        /// <summary>
        ///  controleer leerling in session is uitgebreid met klimatogram, 
        /// Selecteren van een klimatogram werkt.
        /// </summary>

        [TestMethod]
        public void LeerlingInSessionWerdUitgebreidMetKlimatogram()
        {
            string ukkel = "Ukkel";
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object };
            var vmLocatie = new KlimatogramKiezenLocatieViewModel();
            Land land = _graadMock.Object.Continenten.First(c => c.Naam.Equals("Europa")).Landen
                .FirstOrDefault(l => l.Naam.Equals("België"));
            vmLocatie.Locatie = ukkel;
            _klimatogramController.KiesLocatie(leerling, land, vmLocatie);
            Assert.AreEqual(land.Klimatogrammen.First(k => k.Locatie.Equals(ukkel)), leerling.Klimatogram);
        }

        /// <summary>
        /// Een leerling van de derde graad mag geen klimatogram kunnen kiezen.
        /// </summary>
        [TestMethod]
        public void LeerlingVanDerdeGraadMagGeenKlimatogramKunnenKiezen()
        {
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            RedirectToRouteResult result = _klimatogramController.Index(new Leerling() { Graad = _graadMock.Object }) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

    }
}
