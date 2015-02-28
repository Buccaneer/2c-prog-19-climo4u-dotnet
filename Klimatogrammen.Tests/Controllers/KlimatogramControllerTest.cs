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

namespace Klimatogrammen.Tests.Controllers {
    [TestClass]
    public class KlimatogramControllerTest {

        private KlimatogramController _klimatogramController;
        private IEnumerable<Continent> _continenten;
      
        [TestInitialize]
        public void Init() {
            //_sessionRepository = new SessionRepositoryMock();
            _continenten = new ContinentFactory().MaakContinenten();
            _klimatogramController = new KlimatogramController(_continenten);
        }

        /// <summary>
        ///  controleer indien leerling niet in sessie dat redirect naar jaar/graad
        /// </summary>

        [TestMethod]
        public void IndienGeenLeerlingInSessieRedirectNaarGraad() {
            RedirectToRouteResult result = _klimatogramController.Index(null) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        /// <summary>
        /// controleer geeft lijst continenten weer
        /// </summary>
        [TestMethod]
        public void GeeftLijstContinentenWeer() {
            Leerling leerling = new Leerling { Graad = new Graad { Nummer = 2 }, Jaar = 1 };
            ViewResult result = _klimatogramController.Index(leerling) as ViewResult;
            KlimatogramKiezenIndexViewModel kkIVM = result.Model as KlimatogramKiezenIndexViewModel;
            Assert.AreEqual(7, kkIVM.Continenten.Count());
        }

        /// <summary>
        /// controleer geeft correcte lijst met landen voor geselecteerd continent
        /// </summary>
 
        [TestMethod]
        public void GeeftLandenVoorGeselecteerdContinentWeer() {
            Leerling leerling = new Leerling { Graad = new Graad { Nummer = 2 }, Jaar = 1 };
          
            var vmContinent = new KlimatogramKiezenIndexViewModel();
            vmContinent.Continent = "Europa";
            var result = _klimatogramController.Index(leerling,vmContinent) as PartialViewResult;
            var vmLand = result.Model as KlimatogramKiezenLandViewModel;
            Assert.AreEqual(_continenten.First(c => c.Naam.Equals("Europa")).Landen.Count, vmLand.Landen.Count());
        }


        /// <summary>
        /// controleer geeft correcte lijst met locaties (klimatogrammen) voor geselecteerd land
        ///  </summary>
        [TestMethod]
        public void GeeftLocatiesVoorGeselecteerdLandWeer() {
            Leerling leerling = new Leerling { Graad = new Graad { Nummer = 2 }, Jaar = 1 };
            //_sessionRepository["leerling"] = leerling;
            var vmLand = new KlimatogramKiezenLandViewModel();
           
            vmLand.Land = "België";

            Continent continent = _continenten.First(c => c.Naam.Equals("Europa"));
            var result = _klimatogramController.KiesLand(leerling,continent,vmLand) as PartialViewResult;
            var vmLocatie = result.Model as KlimatogramKiezenLocatieViewModel;
            var count = continent.Landen
                .FirstOrDefault(l => l.Naam.Equals("België")).Klimatogrammen.Count;
            Assert.AreEqual(count, vmLocatie.Locaties.Count());
        }

        /// <summary>
        ///  controleer leerling eerste graad krijgt enkel europa als continent
        /// </summary>

        [TestMethod]
        public void LeerlingEersteGraadKanEnkelEuropaAlsContinentKiezen() {
            Leerling leerling = new Leerling { Graad = new Graad { Nummer = 1 } };
           
            var result = _klimatogramController.Index(leerling) as ViewResult;
            var kkIVM = result.Model as KlimatogramKiezenIndexViewModel;
            Assert.AreEqual(1, kkIVM.Continenten.Count());
            Assert.AreEqual("Europa", kkIVM.Continenten.First().Text);
        }

        /// <summary>
        ///  controleer leerling in session is uitgebreid met klimatogram, 
        /// Selecteren van een klimatogram werkt.
        /// </summary>

        [TestMethod]
        public void LeerlingInSessionWerdUitgebreidMetKlimatogram() {
            Leerling leerling = new Leerling { Graad = new Graad { Nummer = 2 }, Jaar = 1 };
            //_sessionRepository["leerling"] = leerling;
            var vmLocatie = new KlimatogramKiezenLocatieViewModel();
            Land land = _continenten.First(c => c.Naam.Equals("Europa")).Landen
                .FirstOrDefault(l => l.Naam.Equals("België"));

            vmLocatie.Locatie = "Ukkel";
            _klimatogramController.KiesLocatie(leerling,land,vmLocatie);
            Assert.AreEqual(land.Klimatogrammen.First(), leerling.Klimatogram);
        }

        /// <summary>
        /// Een leerling van de derde graad mag geen klimatogram kunnen kiezen.
        /// </summary>
        [TestMethod]
        public void LeerlingVanDerdeGraadMagGeenKlimatogramKunnenKiezen() {
            RedirectToRouteResult result = _klimatogramController.Index(new Leerling() { Graad = new Graad { Nummer = 3 } }) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

    }
}
