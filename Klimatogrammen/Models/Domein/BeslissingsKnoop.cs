using System.Collections.Generic;
using System.Linq;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Een knoop waar een beslissing moet worden genomen volgens een vergelijking en waar een Ja of Nee tak uit voor
    /// </summary>
    public class BeslissingsKnoop : DeterminatieKnoop {
        public virtual Vergelijking Vergelijking { get; set; }

        public virtual DeterminatieKnoop JaKnoop { get; set; }

        public virtual DeterminatieKnoop NeeKnoop { get; set; }

        public override DeterminatieKnoop Determineer(Klimatogram klimatogram) {
            if (Vergelijking.BerekenResultaat(klimatogram))
                return JaKnoop.Determineer(klimatogram);
            return NeeKnoop.Determineer(klimatogram);
        }

        public override List<VegetatieType> MaakLijstMetAlleVegetatieTypes()
        {
            return JaKnoop.MaakLijstMetAlleVegetatieTypes().Concat(NeeKnoop.MaakLijstMetAlleVegetatieTypes()).ToList();
        }

        public override void Laad() {

            Vergelijking.Operator = Vergelijking.Operator;
            if (JaKnoop != null)
            JaKnoop.Laad();
            if (NeeKnoop != null)
            NeeKnoop.Laad();
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
