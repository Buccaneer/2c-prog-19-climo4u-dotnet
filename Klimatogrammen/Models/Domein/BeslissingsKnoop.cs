namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Een knoop waar een beslissing moet worden genomen volgens een vergelijking en waar een Ja of Nee tak uit voor
    /// </summary>
    public class BeslissingsKnoop : DeterminatieKnoop {
        public virtual Vergelijking Vergelijking { get; set; }

        public virtual DeterminatieKnoop JaKnoop { get; set; }

        public virtual DeterminatieKnoop NeeKnoop { get; set; }

        public override DeterminatieKnoop NeemNeeKnoop() {
            return NeeKnoop;
        }

        public override DeterminatieKnoop NeemJuisteKnoop(Klimatogram klimatogram) {
            if (Vergelijking.BerekenResultaat(klimatogram))
                return JaKnoop;
            return NeeKnoop;
        }

        public override DeterminatieKnoop NeemJaKnoop() {
            return JaKnoop;
        }

        public BeslissingsKnoop() {
        }

        public BeslissingsKnoop(Vergelijking vergelijking, DeterminatieKnoop jaKnoop, DeterminatieKnoop neeKnoop) {
            Vergelijking = vergelijking;
            JaKnoop = jaKnoop;
            NeeKnoop = neeKnoop;
        }
    }
}
