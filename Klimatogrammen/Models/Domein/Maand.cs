using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.DAL.Mapper;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    /// <summary>
    /// Maand is een klasse waar de neerslag en temperatuur wordt opgeslagen.
    /// </summary>
    public class Maand
    {
        public virtual int MaandId { get; set; }
        public virtual Klimatogram Klimatogram { get; set; }
        
        public virtual string Naam { get; set; }

        public virtual double Temperatuur { get; set; }

        public virtual int Neerslag { get; set; }

        public Maand(Klimatogram k, string naam, double temperatuur, int neerslag)
        {
            Klimatogram = k;
            Naam = naam;
            Temperatuur = temperatuur;
            Neerslag = neerslag;
        }

        public Maand(string naam, double temperatuur, int neerslag) : this(null, naam, temperatuur, neerslag)
        {
        }

        public Maand()
        {
        }


    }
}
