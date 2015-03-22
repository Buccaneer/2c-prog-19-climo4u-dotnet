using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Continent is een klasse waar een lijst van landen wordt bijgehouden en waar landen toegevoegd, opgevraagd of verwijdered kunnen worden
    /// <summary>
    public class Continent {

        private ICollection<Land> _landen;
        private string _naam;

        public Continent() {
            Landen = new List<Land>();
        }

        public Continent(String naam)
        {
            Naam = naam;
            Landen = new List<Land>();
        }

        public string Naam
        {
            get
            {
                return _naam;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("De naam van een continent mag niet null zijn.");
                }
                if (value.Equals(""))
                {
                    throw new ArgumentException("De naam van een continent mag niet leeg zijn.");
                }
                if (Regex.IsMatch(value, "[^ëa-zA-Z -]"))
                {
                    throw new ArgumentException("De naam van een continent mag enkel letters, spaties en koppeltekens bevatten.");
                }
                _naam = value;
            }
        }

        public virtual ICollection<Land> Landen {
            get
            {
                return _landen;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("De collectie met landen mag niet null zijn.");
                }
                _landen = value;
            }
        }

        public void VoegLandToe(Land land)
        {
            if (land == null)
            {
                throw new ArgumentException("Een land mag niet null zijn.");
            }
            Landen.Add(land);
        }

        public void VerwijderLand(Land land)
        {
            Landen.Remove(land);
        }

        public Land GeefLand(string land)
        {
            return Landen.FirstOrDefault(l => l.Naam.Equals(land));
        }
    }
}
