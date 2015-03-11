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
    public class DeterminatieControllerTest
    {
        private DeterminatieController _determinatieController;
        private Mock<Klimatogram> _mockKlimatogram;
        private Mock<Graad> _graadMock;
        private DeterminatieTabel _determinatieTabelMock;
        private GraadMockFactory _graadMockFactory = new GraadMockFactory();

        [TestInitialize]
        public void Init()
        {
            _determinatieController = new DeterminatieController();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            _graadMock = _graadMockFactory.MaakEersteGraadAan();
        }

        [TestMethod]
        public void IndienGeenKlimatogramRedirectNaarKlimatogram()
        {
            RedirectToRouteResult result =
                _determinatieController.Index(new Leerling { Graad = _graadMock.Object }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Klimatogram", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DeterminatieIsNietNull()
        {
            ViewResult result =
                _determinatieController.Index(new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeterminatieWordtWeergegeven()
        {
            ViewResult result =
                _determinatieController.Index(new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as ViewResult;
            DeterminatieIndexViewModel dIVM = result.Model as DeterminatieIndexViewModel;
            Assert.IsTrue(dIVM is DeterminatieIndexViewModel);
        }

        [TestMethod]
        public void ViewVegetatieTypeIsNotNull()
        {
            DeterminatieIndexViewModel determinatieVM = new DeterminatieIndexViewModel();
            Leerling leerling = new Leerling {Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object};
            ViewResult vr =
                _determinatieController.Index(leerling
                   , determinatieVM) as ViewResult;
            
            Assert.IsNotNull(vr);
        }

        [TestMethod]
        public void VegetatieTypeGraad1GeeftPartialViewNameTerug()
        {
            DeterminatieIndexViewModel determinatieVM = new DeterminatieIndexViewModel();
            Leerling leerling = new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object };
            determinatieVM.Correct = true;
            ActionResult ac =
                _determinatieController.Index(leerling
                   , determinatieVM);
            

            Assert.AreEqual("_Graad1", determinatieVM.PartialViewName);
        }

        [TestMethod]
        public void VegetatieTypeGraad2Jaar1GeeftPartialViewNameTerug()
        {
            DeterminatieIndexViewModel determinatieVM = new DeterminatieIndexViewModel();
            _graadMock = _graadMockFactory.MaakTweedeGraadEersteJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object };
            determinatieVM.Correct = true;
            ActionResult ac =
                _determinatieController.Index(leerling
                   , determinatieVM);


            Assert.AreEqual("_Graad2jaar1", determinatieVM.PartialViewName);
        }
        [TestMethod]
        public void VegetatieTypeGraad2Jaar2GeeftPartialViewNameTerug()
        {
            DeterminatieIndexViewModel determinatieVM = new DeterminatieIndexViewModel();
            _graadMock = _graadMockFactory.MaakTweedeGraadTweedeJaarAan();
            Leerling leerling = new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object };
            determinatieVM.Correct = true;
            ActionResult ac =
                _determinatieController.Index(leerling
                   , determinatieVM);
            Assert.AreEqual("_Graad2jaar2", determinatieVM.PartialViewName);
        }

        [TestMethod]
        public void VerbeterGraadVraag2GeeftViewTerug()
        {   VegetatieVraagViewModel VraagVM = new VegetatieVraagViewModel();
            Leerling leerling = new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object };
            ViewResult vr =
                _determinatieController.VerbeterVraagGraad2(leerling
                   , VraagVM) as ViewResult;

            Assert.IsNotNull(vr);
        }

        [TestMethod]
        public void VerbeterGraadVraag2GeeftWaarden()
        {
            VegetatieVraagViewModel VraagVM = new VegetatieVraagViewModel();
            Leerling leerling = new Leerling { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object };
            ViewResult vr =
                _determinatieController.VerbeterVraagGraad2(leerling
                   , VraagVM) as ViewResult;
            DeterminatieIndexViewModel determinatieVM = vr.Model as DeterminatieIndexViewModel;

            Assert.IsNotNull(determinatieVM.Antwoord);
            Assert.IsNotNull(determinatieVM.GebruikersAntwoord);
        }




        [TestMethod]
        public void LeerlingNullRedirectNaarHome()
        {
            //    VraagRepository vRep = VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram);
            RedirectToRouteResult result = _determinatieController.Index(null) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GraadNullRedirectNaarHomeSelectie()
        {
            //    VraagRepository vRep = VraagRepository.CreerVragenVoorKlimatogram(_mockKlimatogram);
            RedirectToRouteResult result = _determinatieController.Index(new Leerling { Graad = null, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void DerdeGraadRedirectNaarLocatieSelectie()
        {
            _graadMock = _graadMockFactory.MaakDerdeGraadAan();
            RedirectToRouteResult result2 = _determinatieController.Index(new Leerling() { Graad = _graadMock.Object, Klimatogram = _mockKlimatogram.Object }) as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            Assert.AreEqual("Index", result2.RouteValues["action"]);
            //Home gaat nog veranderen naar determinatie
            Assert.AreEqual("Home", result2.RouteValues["controller"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetJsonGooitFoutmeldingBijAanroepenZonderleerling()
        {
            // getJson() --> regels GetJson() plaats fail in commentaar.
            //Assert.Fail("getJson() is geen geldige naam :p gelieve GetJson() te gebruiken.");

         
            JsonResult result = _determinatieController.GetJSON(null) as JsonResult;

        }
    }
}
