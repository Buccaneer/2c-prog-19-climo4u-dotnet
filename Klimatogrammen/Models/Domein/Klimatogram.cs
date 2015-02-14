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
        private double[] _gemiddeldeTemperatuur;
        private int[] _gemiddeldeNeerslag;

        public Klimatogram(double[] gemiddeldeTemperatuur, int[] gemiddeldeNeerslag)
        {
            GemiddeldeTemperatuur = gemiddeldeTemperatuur;
            GemiddeldeNeerslag = gemiddeldeNeerslag;
        }

        public double[] GemiddeldeTemperatuur {
            get
            {
                return _gemiddeldeTemperatuur;
            }
            set
            {
                if (value.Length != 12)
                {
                    throw new ArgumentException("Er moeten exact 12 waarden doorgegeven worden.");
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] < -273.15 || value[i] > 500)
                    {
                        throw new ArgumentOutOfRangeException("De waarde van een gemiddelde temperatuur moet tussen -273.15 en 500 liggen.");
                    }
                }
                _gemiddeldeTemperatuur = value;
            }
        }

        public int[] GemiddeldeNeerslag { 
            get
            {
                return _gemiddeldeNeerslag;
            }
            set
            {
                if (value.Length != 12)
                {
                    throw new ArgumentException("Er moeten exact 12 waarden doorgegeven worden.");
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if(value[i] < 0)
                    {
                        throw new ArgumentOutOfRangeException("De waarde van de neerslag per maand mag niet negatief zijn.");
                    }
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
