using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class Graad
    {
        private IEnumerable<Continent> _continenten;
        private int _nummer;
        private IVraagRepository _vraagRepository;

        public Graad()
        {
            _continenten = new Collection<Continent>();
        }

        public int Nummer
        {
            get { return _nummer; }
            set
            {
                if (value > 3 || value < 1)
                    throw new ArgumentException("Graad moet tussen 1 en 3 liggen");
                _nummer = value;
            }
        }

        public IEnumerable<Continent> Continenten
        {
            get
            {
                if (Nummer == 1)
                {
                    return _continenten.Where(c => c.Naam.Equals("Europa"));
                }
                return _continenten;
            }
            set { _continenten = value; }
        }

        public IEnumerable<Vraag> Vragen
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
