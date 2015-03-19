using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Een facade met alle logica en eigenschappen voor een determinatietabel voor te stellen.
    /// </summary>
    public class DeterminatieTabel {
        private Dictionary<Klimatogram, ResultaatBlad> _determinaties;
    
        public int DeterminatieTabelId { get; set; }

        public DeterminatieKnoop BeginKnoop { get; set; }

        /// <summary>
        /// Het systeem determineert een meegegeven klimatogram.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram dat gedetermineerd moet worden.</param>
        /// <returns>ResultaatBlad met het vegetatie- en klimaattype in.</returns>
        public ResultaatBlad Determineer(Klimatogram klimatogram)
        {
            if (!_determinaties.ContainsKey(klimatogram))
            {
                _determinaties.Add(klimatogram, BeginKnoop.Determineer(klimatogram) as ResultaatBlad);
            }
            return _determinaties[klimatogram];
        }

        public Resultaat ValideerGebruikerResultaat(ResultaatBlad gebruikersKnoop, Klimatogram klimatogram)
        {
            return Determineer(klimatogram).Equals(gebruikersKnoop) ? Resultaat.Juist : Resultaat.Fout;
        }

        public object MaakJsonObject()
        {
            return BeginKnoop;
        }

        public IEnumerable<VegetatieType> AlleVegetatieTypes
        {
            get
            {
                return BeginKnoop.MaakLijstMetAlleVegetatieTypes();
            }
        }

        public VegetatieType GeefVegetatieType (Klimatogram klimatogram)
        {
            return Determineer(klimatogram).VegetatieType;
        }

        public DeterminatieTabel(DeterminatieKnoop beginKnoop) {
            BeginKnoop = beginKnoop;
        }

        public DeterminatieTabel()
        {
            _determinaties = new Dictionary<Klimatogram, ResultaatBlad>();
        }
    }
}