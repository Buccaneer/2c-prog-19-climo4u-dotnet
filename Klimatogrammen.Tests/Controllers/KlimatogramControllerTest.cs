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

namespace Klimatogrammen.Tests.Controllers {
    [TestClass]
    public class KlimatogramControllerTest {
        private KlimatogramController _klimatogramController;
        private Mock<IContinentRepository> _mockContinentenRepository;

        [TestInitialize]
        public void Init() {
            //_sessionRepository = new SessionRepositoryMock();
            _mockContinentenRepository = new Mock<IContinentRepository>();
            TrainMock();
            _klimatogramController = new KlimatogramController(_mockContinentenRepository.Object);
        }

        private void TrainMock() {
            IQueryable<Continent> continenten = InitData().AsQueryable();
            _mockContinentenRepository.Setup(k => k.GeefContinenten()).Returns(continenten);
            _mockContinentenRepository.Setup(k => k.GeefContinent("Europa")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Europa")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Noord-Amerika")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Noord-Amerika")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Antarctica")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Antarctica")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Zuid-Amerika")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Zuid-Amerika")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Azië")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Azië")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Oceanië")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Oceanië")));
            _mockContinentenRepository.Setup(k => k.GeefContinent("Afrika")).Returns(continenten.FirstOrDefault(c => c.Naam.Equals("Afrika")));
        }

        private IEnumerable<Continent> InitData() {
            Continent noordAmerika = new Continent("Noord-Amerika");
            Continent zuidAmerika = new Continent("Zuid-Amerika");
            Continent antartica = new Continent("Antartica");
            Continent europa = new Continent("Europa");
            Continent azie = new Continent("Azië");
            Continent afrika = new Continent("Afrika");
            Continent oceanie = new Continent("Oceanië");

            Land belgie = new Land("België");

            Neerslag[] gemiddeldeNeerUkkel = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            Temperatuur[] gemiddeldeTempUkkel = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            belgie.VoegKlimatogramToe(new Klimatogram(gemiddeldeTempUkkel, gemiddeldeNeerUkkel) { Locatie = "Ukkel", BeginJaar = 1961, EindJaar = 2009 });
            europa.VoegLandToe(belgie);

            Land griekenland = new Land("Griekenland");
            griekenland.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 10.0, 10.5, 12.4, 16.0, 20.6, 25.0, 27.8, 24.3, 19.3, 15.4, 12.0, 9.0 },
            new Neerslag[]{45,48,44,25,14,6,6,8,10,48,51,66
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Athene" });

            Land kameroen = new Land("Kameroen");
            kameroen.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 23.7, 25.3, 25.0, 24.6, 24.1, 23.4, 22.6, 23.0, 23.1, 23.3, 23.7, 23.7 },
            new Neerslag[]{17,51,140,180,220,162,70,102,254,296,111,25
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Yaounde" });

            Land brazillie = new Land("Brazillië");
            brazillie.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 21.6, 21.8, 22.0, 21.4, 20.2, 19.1, 19.1, 21.2, 22.5, 22.1, 21.7, 21.5 },
            new Neerslag[]{241,215,189,124,39,9,12,13,52,172,238,249
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Brasilia" });

            Land australie = new Land("Australië");
            australie.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 24.2, 24.6, 22.7, 19.2, 16.0, 13.9, 13.0, 13.2, 14.5, 16.3, 19.1, 21.8 },
            new Neerslag[]{9,14,16,47,103,168,156,111,71,46,23,9
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Perth" });

            Land thailand = new Land("Thailand");
            thailand.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 25.9, 27.4, 28.7, 29.7, 29.2, 28.7, 28.3, 28.1, 27.8, 27.6, 26.9, 25.6 },
            new Neerslag[]{9,30,29,65,220,149,155,197,344,242,48,10
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Bangkok" });
            Land witRusland = new Land("Wit-Rusland");
            witRusland.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { -6.9, -5.8, -1.4, 6.0, 12.9, 16.1, 17.3, 16.5, 11.7, 6.3, 0.8, -3.8 },
            new Neerslag[]{40,34,42,42,62,83,88,72,60,49,52,53
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Minsk" });

            Land rusland = new Land("Rusland");
            rusland.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { -41.0, -36.0, -22.0, -6.1, 6.7, 15.4, 18.7, 14.9, 5.7, -8.5, -29, -39 },
            new Neerslag[]{9,7,6,10,18,37,39,37,29,20,16,12
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Jakutsk" });
            Land vs = new Land("Usa");
            vs.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { -26.0, -27.7, -26.0, -18.8, -6.9, 1.3, 3.9, 2.2, -0.7, -10.1, -18.9, -23.7 },
            new Neerslag[]{4,4,5,5,5,7,25,23,14,11,6,5
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Barrow, Alaska" });

            vs.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { -2.3, 1.2, 5.4, 9.8, 14.9, 20.6, 25.5, 24.2, 18.4, 11.8, 4.9, -1.3 },
            new Neerslag[]{28,31,49,54,46,24,21,22,33,37,33,36
                }
        ) { BeginJaar = 1951, EindJaar = 1990, Locatie = "Salt Lake City" });

            australie.VoegKlimatogramToe(new Klimatogram(
            new Temperatuur[] { 28.7, 27.8, 24.9, 20.2, 15.6, 12.4, 11.6, 14.2, 18.2, 22.8, 25.7, 27.8 },
            new Neerslag[]{42,3,52,17,18,14,16,12,11,19,27,36
                }
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Alice Springs" });

            europa.VoegLandToe(griekenland);
            europa.VoegLandToe(witRusland);
            azie.VoegLandToe(rusland);
            azie.VoegLandToe(thailand);
            noordAmerika.VoegLandToe(vs);
            afrika.VoegLandToe(kameroen);
            zuidAmerika.VoegLandToe(brazillie);
            oceanie.VoegLandToe(australie);
            yield return europa;
            yield return azie;
            yield return noordAmerika;
            yield return afrika;
            yield return zuidAmerika;
            yield return oceanie;
            yield return antartica;
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
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 1 };
            ViewResult result = _klimatogramController.Index(leerling) as ViewResult;
            KlimatogramKiezenIndexViewModel kkIVM = result.Model as KlimatogramKiezenIndexViewModel;
            Assert.AreEqual(7, kkIVM.Continenten.Count());
        }

        /// <summary>
        /// controleer geeft correcte lijst met landen voor geselecteerd continent
        /// </summary>
 
        [TestMethod]
        public void GeeftLandenVoorGeselecteerdContinentWeer() {
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 1 };
          
            var vmContinent = new KlimatogramKiezenIndexViewModel();
            vmContinent.Continent = "Europa";
            var result = _klimatogramController.Index(leerling,vmContinent) as PartialViewResult;
            var vmLand = result.Model as KlimatogramKiezenLandViewModel;
            Assert.AreEqual(_mockContinentenRepository.Object.GeefContinent("Europa").Landen.Count, vmLand.Landen.Count());
        }


        /// <summary>
        /// controleer geeft correcte lijst met locaties (klimatogrammen) voor geselecteerd land
        ///  </summary>
        [TestMethod]
        public void GeeftLocatiesVoorGeselecteerdLandWeer() {
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 1 };
            //_sessionRepository["leerling"] = leerling;
            var vmLand = new KlimatogramKiezenLandViewModel();
           
            vmLand.Land = "België";

            Continent c = _mockContinentenRepository.Object.GeefContinent("Europa");
            var result = _klimatogramController.KiesLand(leerling,c,vmLand) as PartialViewResult;
            var vmLocatie = result.Model as KlimatogramKiezenLocatieViewModel;
            var count = c.Landen
                .FirstOrDefault(l => l.Naam.Equals("België")).Klimatogrammen.Count;
            Assert.AreEqual(count, vmLocatie.Locaties.Count());
        }

        /// <summary>
        ///  controleer leerling eerste graad krijgt enkel europa als continent
        /// </summary>

        [TestMethod]
        public void LeerlingEersteGraadKanEnkelEuropaAlsContinentKiezen() {
            Leerling leerling = new Leerling { Graad = Graad.Een };
           
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
            Leerling leerling = new Leerling { Graad = Graad.Twee, Jaar = 1 };
            //_sessionRepository["leerling"] = leerling;
            var vmLocatie = new KlimatogramKiezenLocatieViewModel();
            Land land = _mockContinentenRepository.Object.GeefContinent("Europa").Landen
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
            RedirectToRouteResult result = _klimatogramController.Index(new Leerling() {Graad=Graad.Drie}) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

    }
}
