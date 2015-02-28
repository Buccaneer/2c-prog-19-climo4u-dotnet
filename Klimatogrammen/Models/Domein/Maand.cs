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
    public class Maand
    {
        public int MaandId { get; set; }
        public Klimatogram Klimatogram { get; set; }
        
        public string Naam { get; set; }

        public double Temperatuur { get; set; }

        public int Neerslag { get; set; }

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
