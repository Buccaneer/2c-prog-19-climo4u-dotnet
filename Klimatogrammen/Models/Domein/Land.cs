using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Klimatogrammen.Models.Domein {
    public class Land {

        private string _naam;
        private ICollection<Klimatogram> _klimatogrammen;

        public Land()
        {
            Klimatogrammen = new List<Klimatogram>();
        }

        public Land(string naam)
        {
            Naam = naam;
            Klimatogrammen = new List<Klimatogram>();
        }

        public String Naam {
            get
            {
                return _naam;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("De naam van een land mag niet null zijn.");
                }
                if (value.Equals(""))
                {
                    throw new ArgumentException("De naam van een land mag niet leeg zijn.");
                }
                if (Regex.IsMatch(value, "[^çéèëa-zA-Z -]"))
                {
                    throw new ArgumentException("De naam van een land mag enkel letters, spaties en koppeltekens bevatten.");
                }
                _naam = value;
            }
        }

        public virtual ICollection<Klimatogram> Klimatogrammen {
            get
            {
                return _klimatogrammen;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("De collectie met klimatogrammen mag niet null zijn.");
                }
                _klimatogrammen = value;
            }
        }

        public void VoegKlimatogramToe(Klimatogram klimatogram)
        {
            if (klimatogram == null)
            {
                throw new ArgumentException("Een klimatogram mag niet null zijn.");
            }
            Klimatogrammen.Add(klimatogram);
            klimatogram.Land = this;
        }

        public void VerwijderKlimatogram(Klimatogram klimatogram)
        {
            Klimatogrammen.Remove(klimatogram);
            klimatogram.Land = null;
        }

        public bool HeeftKlimatogrammen()
        {
            return Klimatogrammen.Any();
        }

        public Klimatogram GeefKlimatogram(string locatie)
        {
            return Klimatogrammen.FirstOrDefault(l => l.Locatie.Equals(locatie));
        }
    }
}
