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
        private int _nummer;

        public Graad()
        {
            Continenten = new List<Continent>();
            Vragen = new List<Vraag>();
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

        public int Jaar { get; set; }

        public virtual ICollection<Continent> Continenten { get; set; }

        public virtual ICollection<Vraag> Vragen { get; set; }
    }
}
