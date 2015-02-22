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


        public Klimatogram() {

        }

        public int BeginJaar { get; set; }
        public int EindJaar { get; set; }
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

        public string Locatie { get; set; }

        public int TotaalNeerslag {
            get
            {
                return _gemiddeldeNeerslag.Sum(n => n.Waarde);
            }
        }

        public double TotaalGemiddeldeTemperatuur {
            get
            {
                return Math.Round(_gemiddeldeTemperatuur.Average(t => t.Waarde),1);
            }
        }

        public DbGeography Coordinaten
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

    }
}
