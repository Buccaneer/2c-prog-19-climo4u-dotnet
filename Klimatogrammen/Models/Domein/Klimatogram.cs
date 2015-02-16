using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;

namespace Klimatogrammen {
    public class Klimatogram
    {
        private ICollection<double> _gemiddeldeTemperatuur = new List<double>();
        private ICollection<int> _gemiddeldeNeerslag = new List<int>();

        public Klimatogram(ICollection<double> gemiddeldeTemperatuur, ICollection<int> gemiddeldeNeerslag)
        {
            GemiddeldeTemperatuur = gemiddeldeTemperatuur;
            GemiddeldeNeerslag = gemiddeldeNeerslag;
        }


        public Klimatogram() {

        }

        public ICollection<double> GemiddeldeTemperatuur {
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
                if (value.Any(i => i < -273.15 || i > 500))
                    {
                        throw new ArgumentOutOfRangeException("De waarde van een gemiddelde temperatuur moet tussen -273.15 en 500 liggen.");
                    }
                
                _gemiddeldeTemperatuur = value;
            }
        }

        public ICollection<int> GemiddeldeNeerslag { 
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
            if (value.Any(i => i < 0))
                    {
                        throw new ArgumentOutOfRangeException("De waarde van de neerslag per maand mag niet negatief zijn.");
                    }
                
                _gemiddeldeNeerslag = value;
            }
        }

        public string Locatie { get; set; }

        public int TotaalNeerslag {
            get
            {
                return _gemiddeldeNeerslag.Sum();
            }
        }

        public double TotaalGemiddeldeTemperatuur {
            get
            {
                return _gemiddeldeTemperatuur.Average();
            }
        }

    }
}
