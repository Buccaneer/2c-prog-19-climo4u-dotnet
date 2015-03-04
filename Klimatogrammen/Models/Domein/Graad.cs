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
         
        }

        public virtual int Nummer
        {
            get { return _nummer; }
            set
            {
                if (value > 3 || value < 1)
                    throw new ArgumentException("Graad moet tussen 1 en 3 liggen");
                _nummer = value;
            }
        }

        public virtual int Jaar { get; set; }

        public DeterminatieTabel DeterminatieTabel { get; set; }

        public virtual ICollection<Continent> Continenten { get; set; }

        public virtual ICollection<Vraag> Vragen { get; set; }

        public Continent GeefContinent(string continent)
        {
            return Continenten.First(c => c.Naam.Equals(continent));
        }

        public ICollection<Continent> GeefContinenten()
        {
            return Continenten;
        }

        public string[] ValideerVragen(string[] antwoord, Klimatogram klimatogram)
        {
            string[] antwoorden = new string[antwoord.Length];

            for (int i = 0; i < Vragen.Count; i++)
            {
                Vragen.ElementAt(i).ValideerVraag(antwoord[i], klimatogram);
                if (Vragen.ElementAt(i).Resultaat == Resultaat.Juist)
                {
                    antwoorden[i] = antwoord[i];
                }
                else
                {
                    antwoorden[i] = null;
                }
            }

            return antwoorden;
        }

        public ICollection<Vraag> GeefVragen()
        {
            return Vragen;
        }
    }
}
