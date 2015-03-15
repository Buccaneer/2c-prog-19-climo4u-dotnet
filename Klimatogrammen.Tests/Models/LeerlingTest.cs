using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models {
    [TestClass]
    public class LeerlingTest {
        [TestMethod]
        public void GeefKlimatogrammenDerdeGraadGeeftZesKlimatogrammen() {
           GraadMockFactory graadMockFactory =new GraadMockFactory();
            var graad = graadMockFactory.MaakDerdeGraadAan().Object;

            Leerling l = new Leerling();
            l.Graad = graad;

            var klimatogrammen = l.GeefKlimatogrammenDerdeGraad();

            var vegTypes = new List<string>(6);

            Assert.AreEqual(6,klimatogrammen.Count);

            foreach (var klimatogram in klimatogrammen) {
                var res = l.Graad.DeterminatieTabel.Determineer(klimatogram).VegetatieType.Naam;
                if (vegTypes.Contains(res))
                    Assert.Fail("Vegetatietypes zijn niet uniek.");
                else
                    vegTypes.Add(res);

            }
        }

        [TestMethod]
        public void GeefKlimatogrammenDerdeGraadStoptIndienErGeenZesMogelijkhedenZijn() {
            GraadMockFactory graadMockFactory = new GraadMockFactory();
            var graad = graadMockFactory.MaakEersteGraadAan().Object;

            Leerling l = new Leerling();
            l.Graad = graad;

            var klimatogrammen = l.GeefKlimatogrammenDerdeGraad();
            Assert.AreEqual(1, klimatogrammen.Count);
        }
    }
}
