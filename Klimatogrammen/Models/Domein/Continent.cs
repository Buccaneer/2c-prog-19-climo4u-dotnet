using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Klimatogrammen.Models.Domein {
    public class Continent {

        private ICollection<Land> _landen;
        private String _naam;

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
                if (Regex.IsMatch(value, "[^a-zA-Z ]"))
                {
                    throw new ArgumentException("De naam van een continent mag enkel letters en spaties bevatten.");
                }
                _naam = value;
            }
        }

        public ICollection<Land> Landen {
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

        public void voegLandToe(Land land)
        {
            if (land == null)
            {
                throw new ArgumentException("Een land mag niet null zijn.");
            }
            Landen.Add(land);
        }

        public void verwijderLand(Land land)
        {
            Landen.Remove(land);
        }
    }
}
