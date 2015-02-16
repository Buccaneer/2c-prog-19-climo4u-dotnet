using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen {
    public class Klimatogram
    {
        private System.Collections.Generic.ICollection<Temperatuur> _gemiddeldeTemperatuur = new List<Temperatuur>();
        private System.Collections.Generic.ICollection<Neerslag> _gemiddeldeNeerslag = new List<Neerslag>();

        public Klimatogram(ICollection<Temperatuur> gemiddeldeTemperatuur, ICollection<Neerslag> gemiddeldeNeerslag)
        {
            GemiddeldeTemperatuur = gemiddeldeTemperatuur;
            GemiddeldeNeerslag = gemiddeldeNeerslag;
        }


        public Klimatogram() {

        }

        public System.Collections.Generic.ICollection<Temperatuur> GemiddeldeTemperatuur {
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

        public System.Collections.Generic.ICollection<Neerslag> GemiddeldeNeerslag { 
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
                return _gemiddeldeTemperatuur.Average(t => t.Waarde);
            }
        }

    }
}
