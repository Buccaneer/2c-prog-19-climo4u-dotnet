namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Stelt een resultaat van een determinatie voor. Heeft een Vegetatietype en een klimaattype.
    /// </summary>
    public class ResultaatKnoop : DeterminatieKnoop {

        public string VegetatieType { get; private set; }

        public string KlimaatType { get; private set; }

        public ResultaatKnoop() {
        }

        public ResultaatKnoop(string vegetatieType, string klimaatType) {
            VegetatieType = vegetatieType;
            KlimaatType = klimaatType;
        }
    }
}
