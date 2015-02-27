using System;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Abstracte componentklasse, vormt basis tot de composite voor de determinatietabel voor te stellen.
    /// </summary>
    public abstract class DeterminatieKnoop {
        /// <summary>
        /// Retourneer volgens de opgeloste vergelijking en het klimatogram de juiste Knoop.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram waaruit de gegevens moeten worden gehaald.</param>
        /// <returns>Volgende knoop of leaf punt in de determinatietabel.</returns>
        public DeterminatieKnoop NeemJuisteKnoop(Klimatogram klimatogram) {
            throw new NotSupportedException();
        }

        /// <summary>
        /// De gebruiker selecteerd Ja, het systeem retourneerd de volgende Ja knoop.
        /// </summary>
        /// <returns>Volgende knoop of leaf punt in de determinatietabel.</returns>
        public DeterminatieKnoop NeemJaKnoop() {
            throw new NotSupportedException();
        }

        /// <summary>
        /// De gebruiker selecteerd Nee, het systeem retourneerd de volgende Nee knoop.
        /// </summary>
        /// <returns>Volgende knoop of leaf punt in de determinatietabel.</returns>
        public DeterminatieKnoop NeemNeeKnoop() {
            throw new NotSupportedException();
        }
    }
}
