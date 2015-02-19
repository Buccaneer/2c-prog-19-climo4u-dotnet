using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Klimatogrammen.Infrastructure;
using System.Web.Mvc;
using Klimatogrammen.Controllers;
using Klimatogrammen.ViewModels;
using System.Collections.Generic;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Controllers
{
    [TestClass]
    public class KlimatogramControllerTest
    {
        private KlimatogramController klimatogramController;
        private ISessionRepository _sessionRepository;

        [TestInitialize]
        public void Init()
        {
            _sessionRepository = new SessionRepositoryMock();
            //klimatogramController = new KlimatogramController(_sessionRepository);
        }

        [TestMethod]
        public void IndexIsNietNull()
        {
            ViewResult result = klimatogramController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexHttpPostRedirectNaarLanden()
        {

        }

        [TestMethod]
        public void ContinentZitInSessieNaCorrectePost()
        {
            
        }

        [TestMethod]
        public void IndexHttpPostRedirectNaarLocaties()
        {
            
        }

        [TestMethod]
        public void LandZitInSessieNaCorrectePost()
        {
            
        }

        [TestMethod]
        public void IndexHttpPostRedirectKlimatogram()
        {
            
        }

        [TestMethod]
        public void KlimatogramZitInSessieNaCorrectePost()
        {
            
        }

    }
}
