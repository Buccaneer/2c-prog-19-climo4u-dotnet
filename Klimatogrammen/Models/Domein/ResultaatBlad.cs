using System;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Stelt een resultaat van een determinatie voor. Heeft een Vegetatietype en een klimaattype.
    /// </summary>
    public class ResultaatBlad : DeterminatieKnoop {

        public string VegetatieType { get; private set; }

        public string KlimaatType { get; private set; }

        public override DeterminatieKnoop Determineer(Klimatogram klimatogram) {
            return this;
        }

        public ResultaatBlad() { }

        public ResultaatBlad(string vegType, string klimType) {
            VegetatieType = vegType;
            KlimaatType = klimType;
        }

       
    }
}
