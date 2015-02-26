using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Klimatogrammen.Models.Domein
{
    public class Klimatogram
    {

        public Klimatogram(ICollection<Maand> maanden)
        {
            Maanden = maanden;
        }

        public Klimatogram(ICollection<Maand> maanden , DbGeography coordinaten)
        {
            Maanden = maanden;
            Coordinaten = coordinaten;
        }


        public Klimatogram()
        {

        }

        public virtual int BeginJaar { get; set; }
        public virtual int EindJaar { get; set; }
        public Land Land { get; internal set; }

        public DbGeography Coordinaten { get; set; }

        public IEnumerable<Maand> Maanden { get; set; }

        public virtual string Locatie { get; set; }

        public ICollection<double> GeefTemperaturen()
        {
            return Maanden.Select(maand => maand.Temperatuur).ToList();
        }

        public ICollection<int> GeefNeerslagen()
        {
            return Maanden.Select(maand => maand.Neerslag).ToList();
        }

        public int GeefTotaleNeerslag()
        {
            return Maanden.Sum(maand => maand.Neerslag);
        }

        public double GeefGemiddeldeTemperatuur()
        {
            return Maanden.Average(maand => maand.Temperatuur);
        }
    }
}
