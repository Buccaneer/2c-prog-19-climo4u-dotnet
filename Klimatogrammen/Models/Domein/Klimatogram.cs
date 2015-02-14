using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

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
                    if (value[i] < 0)
                    {
                        throw new ArgumentOutOfRangeException("De waarde van een gemiddelde temperatuur mag niet negatief zijn.");
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
                int _totaalNeerslag = 0;
                for (int i = 0; i < _gemiddeldeNeerslag.Length; i++)
                {
                    _totaalNeerslag += _gemiddeldeNeerslag[i];
                }
                    return _totaalNeerslag;
            }
        }

        public double TotaalGemiddeldeTemperatuur {
            get
            {
                double _totaalGemiddeldeTemperatuur = 0;
                for (int i = 0; i < _gemiddeldeTemperatuur.Length; i++)
                {
                    _totaalGemiddeldeTemperatuur += _gemiddeldeTemperatuur[i];
                }
                return _totaalGemiddeldeTemperatuur / ((double) _gemiddeldeTemperatuur.Length);
            }
        }

    }
}
