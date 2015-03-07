using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Een facade met alle logica en eigenschappen voor een determinatietabel voor te stellen.
    /// </summary>
    public class DeterminatieTabel {
        private ResultaatBlad _juisteDeterminatie;
    
        public int DeterminatieTabelId { get; set; }

        public DeterminatieKnoop BeginKnoop { get; set; }

        /// <summary>
        /// Het systeem determineert een meegegeven klimatogram.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram dat gedetermineerd moet worden.</param>
        /// <returns>ResultaatBlad met het vegetatie- en klimaattype in.</returns>
        public ResultaatBlad Determineer(Klimatogram klimatogram) {
            return _juisteDeterminatie =  BeginKnoop.Determineer(klimatogram) as ResultaatBlad;
        }

        public Resultaat ValideerGebruikerResultaat(ResultaatBlad gebruikersKnoop) {
            return _juisteDeterminatie == gebruikersKnoop ? Resultaat.Juist : Resultaat.Fout;
        }

        public object MaakJsonObject()
        {
            return BeginKnoop;
        }

        public string[] GeefAlleVegetatieTypes()
        {
            throw new System.NotImplementedException();
        }

        public DeterminatieTabel(DeterminatieKnoop beginKnoop) {
            BeginKnoop = beginKnoop;
        }

        public DeterminatieTabel() { }
    }
}