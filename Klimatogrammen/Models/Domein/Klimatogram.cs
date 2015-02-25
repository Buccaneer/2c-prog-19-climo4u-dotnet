using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Klimatogrammen.Models.Domein {
    public class Klimatogram
    {
        private ICollection<Temperatuur> _gemiddeldeTemperatuur = new List<Temperatuur>();
        private ICollection<Neerslag> _gemiddeldeNeerslag = new List<Neerslag>();

        public Klimatogram(ICollection<Temperatuur> gemiddeldeTemperatuur, ICollection<Neerslag> gemiddeldeNeerslag)
        {
            GemiddeldeTemperatuur = gemiddeldeTemperatuur;
            GemiddeldeNeerslag = gemiddeldeNeerslag;
        }

        public Klimatogram(ICollection<Temperatuur> gemiddeldeTemperatuur, ICollection<Neerslag> gemiddeldeNeerslag, DbGeography coordinaten)
        {
            GemiddeldeTemperatuur = gemiddeldeTemperatuur;
            GemiddeldeNeerslag = gemiddeldeNeerslag;
            Coordinaten = coordinaten;
        }


        public Klimatogram() {

        }

        public virtual int BeginJaar { get; set; }
        public virtual int EindJaar { get; set; }
        public Land Land { get; internal set; }

        public virtual System.Collections.Generic.ICollection<Temperatuur> GemiddeldeTemperatuur {
            get
            {
                return _gemiddeldeTemperatuur;
            }
            set
            {
                if (value.Count != 12)
                {
                    throw new ArgumentException("Er moeten exact 12 waarden doorgegeven worden.");
                }
                
                _gemiddeldeTemperatuur = value;
            }
        }

        public virtual System.Collections.Generic.ICollection<Neerslag> GemiddeldeNeerslag { 
            get
            {
                return _gemiddeldeNeerslag;
            }
            set
            {
                if (value.Count != 12)
                {
                    throw new ArgumentException("Er moeten exact 12 waarden doorgegeven worden.");
                }
   
                _gemiddeldeNeerslag = value;
            }
        }

        public virtual string Locatie { get; set; }

        public virtual int TotaalNeerslag {
            get
            {
                return _gemiddeldeNeerslag.Sum(n => n.Waarde);
            }
        }

        public virtual double TotaalGemiddeldeTemperatuur {
            get
            {
                return Math.Round(_gemiddeldeTemperatuur.Average(t => t.Waarde),1);
            }
        }

        public DbGeography Coordinaten { get; set; }

    }
}
