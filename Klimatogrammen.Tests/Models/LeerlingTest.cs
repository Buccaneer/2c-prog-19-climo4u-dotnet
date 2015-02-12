using System;
using System.Collections;
using System.Collections.ObjectModel;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class LeerlingTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndienLeerlingTweedeGraadMagJaarNietNullZijn()
        {
            Leerling l = new Leerling();
            l.Graad = Graad.Twee;
            l.Jaar = null;
        }

        [TestMethod]
        public void LeerlingTweedeGraadIsCorrect()
        {
            Leerling l = new Leerling() { Graad = Graad.Twee, Jaar = 1 };

            Assert.AreEqual(l.Graad, Graad.Twee);
            Assert.AreEqual(l.Jaar, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingMetVerplichtJaarOnderDeGrensWaarde()
        {
            Leerling l = new Leerling() { Graad = Graad.Twee, Jaar = 0 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingMetVerplichtJaarBovenDeGrensWaarde()
        {
            Leerling l = new Leerling() { Graad = Graad.Twee, Jaar = 3 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingEersteGraadMagGeenJaarMeeKrijgen()
        {
            Leerling l = new Leerling() { Graad = Graad.Een, Jaar = 1 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingDerdeGraadMagGeenJaarMeeKrijgen()
        {
            Leerling l = new Leerling() { Graad = Graad.Drie, Jaar = 1 };
        }


    }
}
