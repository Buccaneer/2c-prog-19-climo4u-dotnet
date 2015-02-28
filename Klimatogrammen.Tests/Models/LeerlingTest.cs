using System;
using System.Collections;
using System.Collections.ObjectModel;
using Klimatogrammen.Models.Domein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klimatogrammen.Tests.Models
{
    /// <summary>
    /// Deze testklasse bevat testen voor de klasse Leerling
    /// Volgende zaken worden getest:
    ///         * Tweede graad -> jaar niet null
    ///         * Tweede graad met correct jaar
    ///         * Tweede graad met jaar onder de grenswaarde
    ///         * Tweede graad met jaar boven de grenswaarde
    ///         * Eerste graad met jaar
    ///         * Derde graad met jaar
    /// </summary>
    [TestClass]
    public class LeerlingTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndienLeerlingTweedeGraadMagJaarNietNullZijn()
        {
            Leerling l = new Leerling {Graad = new Graad {Nummer = 2}, Jaar = null};
        }

        [TestMethod]
        public void LeerlingTweedeGraadIsCorrect()
        {
            Leerling l = new Leerling() { Graad = new Graad { Nummer = 2 }, Jaar = 1 };
            Assert.AreEqual(l.Graad.Nummer, 2);
            Assert.AreEqual(l.Jaar, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingTweedeGraadMetJaarOnderDeGrensWaarde()
        {
            Leerling l = new Leerling() { Graad = new Graad{ Nummer = 2 }, Jaar = 0 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingTweedeGraadMetJaarBovenDeGrensWaarde()
        {
            Leerling l = new Leerling() { Graad = new Graad { Nummer = 2 }, Jaar = 3 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingEersteGraadMagGeenJaarMeeKrijgen()
        {
            Leerling l = new Leerling() { Graad = new Graad { Nummer = 1 }, Jaar = 1 };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeerlingDerdeGraadMagGeenJaarMeeKrijgen()
        {
            Leerling l = new Leerling() { Graad = new Graad { Nummer = 3 }, Jaar = 1 };
        }


    }
}
