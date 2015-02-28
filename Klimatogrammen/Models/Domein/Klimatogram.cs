using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Models.Domein
{
    public class Klimatogram
    {
        private ICollection<Maand> _maanden; 
        public virtual int BeginJaar { get; set; }
        public virtual int EindJaar { get; set; }
        public Land Land { get; internal set; }
        public DbGeography Coordinaten { get; set; }
        public ICollection<Maand> Maanden
        {
            get { return _maanden; }
            set
            {
                value.ForEach(m => m.Klimatogram = this);
                _maanden = value;
            }
        }
        public virtual string Locatie { get; set; }

        public Klimatogram(ICollection<Maand> maanden, DbGeography coordinaten)
        {
            maanden.ForEach(m => m.Klimatogram = this);
            Maanden = maanden;
            Coordinaten = coordinaten;
        }

        public Klimatogram(ICollection<Maand> maanden) : this(maanden, null)
        {
        }

        public Klimatogram()
        {
        }

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
