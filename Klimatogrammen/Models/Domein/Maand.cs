using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class Maand
    {

        public Klimatogram Klimatogram { get; set; }
        
        public string Naam { get; set; }

        public double Temperatuur { get; set; }

        public int Neerslag { get; set; }
    }
}
