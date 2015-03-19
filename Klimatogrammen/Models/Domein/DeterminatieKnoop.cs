using System;
using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Abstracte componentklasse, vormt basis tot de composite voor de determinatietabel voor te stellen.
    /// </summary>
    public abstract class DeterminatieKnoop {
        public virtual int DeterminatieKnoopId { get; set; }

        /// <summary>
        /// Retourneer volgens de opgeloste vergelijking en het klimatogram de juiste Knoop.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram waaruit de gegevens moeten worden gehaald.</param>
        /// <returns>Volgende knoop of leaf punt in de determinatietabel.</returns>
        public abstract DeterminatieKnoop Determineer(Klimatogram klimatogram);

        public abstract List<VegetatieType> MaakLijstMetAlleVegetatieTypes();

        public abstract void Laad();
    }
}
