using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Een facade met alle logica en eigenschappen voor een determinatietabel voor te stellen.
    /// </summary>
    public class DeterminatieTabel {
        public int DeterminatieTabelId { get; set; }

        public DeterminatieKnoop BeginKnoop { get; set; }

        private LinkedList<DeterminatieKnoop> _gebruikersPad;

        private LinkedList<DeterminatieKnoop> _juistePad;

        /// <summary>
        /// Het systeem determineert een meegegeven klimatogram.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram dat gedetermineerd moet worden.</param>
        /// <returns>ResultaatKnoop met het vegetatie- en klimaattype in.</returns>
        public ResultaatKnoop Determineer(Klimatogram klimatogram) {
            _juistePad = new LinkedList<DeterminatieKnoop>();
            _juistePad.AddLast(BeginKnoop);

            while (!(_juistePad.Last.Value is ResultaatKnoop)) 
                _juistePad.AddLast(_juistePad.Last.Value.NeemJuisteKnoop(klimatogram));

            return _juistePad.Last.Value as ResultaatKnoop;
        }

        /// <summary>
        /// De gebruiker selecteert de Ja tak van een vergelijking.
        /// </summary>
        public void NeemJaKnoop() {
            MaakGebruikersPad();

            _gebruikersPad.AddLast(_gebruikersPad.Last.Value.NeemJaKnoop());
        }

        /// <summary>
        /// De gebruiker selecteert een Nee tak van een vergelijking.
        /// </summary>
        public void NeemNeeKnoop() {
            MaakGebruikersPad();

            _gebruikersPad.AddLast(_gebruikersPad.Last.Value.NeemNeeKnoop());
        }

        /// <summary>
        /// 1. Maakt een gebruikerspad linkedlijst aan als deze nog niet bestaat.
        /// 2. Als de gebruikerspad gelinktelijst leeg is, voeg de beginKnoop toe.
        /// </summary>
        private void MaakGebruikersPad() {
            if (_gebruikersPad == null)
                _gebruikersPad = new LinkedList<DeterminatieKnoop>();
            if (_gebruikersPad.Count == 0)
                _gebruikersPad.AddLast(BeginKnoop);
        }

        /// <summary>
        /// Valideert het door gebruiker gekozen pad en wanneer er een fout in zit splits het gebruikerspad op en onthoud enkel het juiste stuk.
        /// </summary>
        public Resultaat ValideerGebruikersPad() {
            LinkedListNode<DeterminatieKnoop> leerling = _gebruikersPad.First;
            LinkedListNode<DeterminatieKnoop> juist = _juistePad.First;

            while (leerling != null && juist != null) {
                if (leerling.Value != juist.Value) {
                    LinkedListNode<DeterminatieKnoop> tijdelijkVolgende = leerling.Next;
                    _gebruikersPad.Remove(leerling);
                    leerling = tijdelijkVolgende;
                } else {
                    leerling = leerling.Next;
                    juist = juist.Next;
                }
            }

            return _gebruikersPad.Last.Value == _juistePad.Last.Value ? Resultaat.Juist : Resultaat.Fout;
        }

        /// <summary>
        /// Heeft de gebruiker een eindpunt (ResultaatKnoop) bereikt.
        /// </summary>
        /// <returns>True of false afhankelijk van hierboven.</returns>
        public bool HeeftLeerlingEindeBereikt() {
            MaakGebruikersPad();

            return _gebruikersPad.Last.Value is ResultaatKnoop;
        }

        /// <summary>
        /// Returneert de knoop of leaf waar de gebruiker zich momenteel bevindt in de determinatietabel.
        /// </summary>
        public DeterminatieKnoop GeefDeterminatieGebruiker() {
            MaakGebruikersPad();

            return _gebruikersPad.Last.Value;
        }

        public DeterminatieTabel(DeterminatieKnoop beginKnoop) {
            BeginKnoop = beginKnoop;
        }

        public DeterminatieTabel() { }
    }
}