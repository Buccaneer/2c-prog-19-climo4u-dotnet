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
    public class Maand : IComparable<Maand>
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


        public int CompareTo(Maand other)
        {
            return GeefWaarde(Naam) < GeefWaarde(other.Naam) ? -1 : 1;
        }

        private int GeefWaarde(String maand)
        {
            int waarde = -1;
            switch (maand)
            {
                case "Januari":
                    waarde = 1;
                    break;
                case "Februari":
                    waarde = 2;
                    break;
                case "Maart":
                    waarde = 3;
                    break;
                case "April":
                    waarde = 4;
                    break;
                case "Mei":
                    waarde = 5;
                    break;
                case "Juni":
                    waarde = 6;
                    break;
                case "Juli":
                    waarde = 7;
                    break;
                case "Augustus":
                    waarde = 8;
                    break;
                case "September":
                    waarde = 9;
                    break;
                case "Oktober":
                    waarde = 10;
                    break;
                case "November":
                    waarde = 11;
                    break;
                case "December":
                    waarde = 12;
                    break;
            }
            return waarde;
        }
    }
}
