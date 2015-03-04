using System;
using System.Collections;
using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein
{

    public class Leerling
    {

        #region Properties
        /// <summary>
        /// Dit is een property die het gekozen klimatogram van de leerling bijhoudt
        /// </summary>
        public Klimatogram Klimatogram { get; set; }

        public Graad Graad { get; set; }
        #endregion

        public Continent GeefContinent(string continent)
        {
            return Graad.GeefContinent(continent);
        }

        public ICollection<Continent> GeefContinenten()
        {
            return Graad.GeefContinenten();
        }

        public string[] ValideerVragen(string[] antwoord)
        {
            return Graad.ValideerVragen(antwoord, Klimatogram);
        }

        public ICollection<Vraag> GeefVragen()
        {
            return Graad.GeefVragen();
        }
    }
}
