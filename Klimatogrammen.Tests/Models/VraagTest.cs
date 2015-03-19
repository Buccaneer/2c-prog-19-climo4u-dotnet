using System;
using System.Collections;
using System.Linq;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Klimatogrammen.Models.Domein;
using System.Collections.Generic;

namespace Klimatogrammen.Tests.Models {
    [TestClass]
    public class VraagTest {

        private Mock<Parameter> _mockParameter;
        private Mock<Klimatogram> _mockKlimatogram;
        private Vraag _vraag;

        [TestInitialize]
        public void Init() {
            ParameterMockFactory pmf = new ParameterMockFactory();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            _mockKlimatogram = kmf.MaakKlimatogramMock();
            _mockParameter = pmf.MaakParameterMock();
            _vraag = new Vraag() {Parameter = _mockParameter.Object};
        }

        #region "Vraagtekst string controle"
        // Hoeft niet DB.
        #endregion

        #region "Vraag controle"

        [TestMethod]
        public void VraagValiderenBijJuistAntwoord() {
            var verwacht = Resultaat.Juist;
            
            Assert.AreEqual(verwacht, _vraag.ValideerVraag("Item2", _mockKlimatogram.Object));
        }

        [TestMethod]
        public void VraagValiderenBijFoutAntwoord() {
            var verwacht = Resultaat.Fout;

            Assert.AreEqual(verwacht, _vraag.ValideerVraag("Item3", _mockKlimatogram.Object));
        }

        [TestMethod]
        public void VraagValiderenRoeptNodigeMethodesOp() {
            _vraag.ValideerVraag("Item2", _mockKlimatogram.Object);


         _mockParameter.Verify(p => p.BerekenWaarde(_mockKlimatogram.Object));
        }
        #endregion
    }
}
