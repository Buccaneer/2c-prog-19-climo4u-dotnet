using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using Klimatogrammen.Models.Domein;
using WebGrease.Css.Extensions;

namespace Klimatogrammen
{
    public class ParameterWarmsteMaand : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            Maand max =  klimatogram.Maanden.First(m => m.Temperatuur.Equals(klimatogram.Maanden.Max(t => t.Temperatuur)));
            StringBuilder sb = new StringBuilder();
            bool first = true;

            foreach (Maand maand in klimatogram.Maanden.Where(maand => maand.Temperatuur.Equals(max.Temperatuur))) {
                if (!first)
                    sb.Append("|");
                else
                    first = false;
                sb.Append(maand.Naam);
              
                
            }


            return sb.ToString();

        }
        public override string GeefBeschrijving()
        {
            return "Warmste Maand";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            klimatogram.Maanden.ForEach(m => mogelijkeAntwoorden.Add(m.Naam));
            return mogelijkeAntwoorden;
        }
    }

    public class ParameterKoudsteMaand : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            Maand min = klimatogram.Maanden.First(m => m.Temperatuur.Equals(klimatogram.Maanden.Min(t => t.Temperatuur)));
            StringBuilder sb = new StringBuilder();

            bool first = true;

            foreach (Maand maand in klimatogram.Maanden.Where(maand => maand.Temperatuur.Equals(min.Temperatuur))) {
                if (!first)
                    sb.Append("|");
                else
                    first = false;
                sb.Append(maand.Naam);


            }


            return sb.ToString();
        }
        public override string GeefBeschrijving()
        {
            return "Koudste Maand";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            klimatogram.Maanden.ForEach(m => mogelijkeAntwoorden.Add(m.Naam));
            return mogelijkeAntwoorden;
        }
    }

    public class ParameterTemperatuurWarmsteMaand : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.Max(m => m.Temperatuur);
        }
        public override string GeefBeschrijving()
        {
            return "Tw";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            klimatogram.Maanden.ForEach(m => mogelijkeAntwoorden.Add(m.Temperatuur.ToString()));
            return mogelijkeAntwoorden;
        }
    }

    public class ParameterTemperatuurKoudsteMaand : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.Min(m => m.Temperatuur);
                }
        public override string GeefBeschrijving()
        {
            return "Tk";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            klimatogram.Maanden.ForEach(m => mogelijkeAntwoorden.Add(m.Temperatuur.ToString()));
            return mogelijkeAntwoorden;
        }
    }

    public class ParameterNeerslagZomer : Parameter
    {
        private readonly string[] _zomerNoorderBreedte =
        {
            "April",
            "Mei",
            "Juni",
            "Juli",
            "Augustus",
            "September"
        };
        private readonly string[] _zomerZuiderBreedte =
        {
            "Januari",
            "Februari",
            "Maart",
            "Oktober",
            "November",
            "December"
        };

        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return BerekenNeerslagZomer(klimatogram, true);
        }
        public override string GeefBeschrijving()
        {
            return "Nz";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            string neerslagCorrect = BerekenNeerslagZomer(klimatogram, true).ToString();
            string neerslagIncorrect = BerekenNeerslagZomer(klimatogram, false).ToString();
            if (new Random().Next(0, 2) == 0) //willekeurige volgorde
            {
                mogelijkeAntwoorden.Add(neerslagCorrect);
                mogelijkeAntwoorden.Add(neerslagIncorrect);
            }
            else
            {
                mogelijkeAntwoorden.Add(neerslagIncorrect);
                mogelijkeAntwoorden.Add(neerslagCorrect);
            }
            return mogelijkeAntwoorden;
        }

        private int BerekenNeerslagZomer(Klimatogram klimatogram, bool correct)
        {
            var latitude = klimatogram.Latitude;
            if (latitude == null)
            {
                throw new ArgumentException("De coördinaten van het Klimatogram geven geen correcte breedtegraad terug");
            }
            if (!correct)
            {
                latitude = latitude == 0 ? -1 : -latitude;
            }
            int neerslag = 0;
            foreach (Maand m in klimatogram.Maanden)
            {
                if (latitude >= 0)
                {
                    if (_zomerNoorderBreedte.Contains(m.Naam)) neerslag += m.Neerslag;
                }
                else
                {
                    if (_zomerZuiderBreedte.Contains(m.Naam)) neerslag += m.Neerslag;
                }
            }
            return neerslag;
        }
    }

    public class ParameterNeerslagWinter : Parameter
    {
        private readonly string[] _winterNoorderBreedte =
        {
            "Januari",
            "Februari",
            "Maart",
            "Oktober",
            "November",
            "December"
        };

        private readonly string[] _winterZuiderBreedte =
        {
            "April",
            "Mei",
            "Juni",
            "Juli",
            "Augustus",
            "September"
        };

        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return BerekenNeerslagWinter(klimatogram, true);
        }
        public override string GeefBeschrijving()
        {
            return "Nw";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            string neerslagCorrect = BerekenNeerslagWinter(klimatogram, true).ToString();
            string neerslagIncorrect = BerekenNeerslagWinter(klimatogram, false).ToString();
            if (new Random().Next(0, 2) == 0) //willekeurige volgorde
            {
                mogelijkeAntwoorden.Add(neerslagCorrect);
                mogelijkeAntwoorden.Add(neerslagIncorrect);
            }
            else
            {
                mogelijkeAntwoorden.Add(neerslagIncorrect);
                mogelijkeAntwoorden.Add(neerslagCorrect);
            }
            return mogelijkeAntwoorden;
        }

        private int BerekenNeerslagWinter(Klimatogram klimatogram, bool correct)
        {
            var latitude = klimatogram.Latitude;
            if (latitude == null)
            {
                throw new ArgumentException("De coördinaten van het Klimatogram geven geen correcte breedtegraad terug");
            }
            if (!correct)
            {
                latitude = latitude == 0 ? -1 : -latitude;
            }
            int neerslag = 0;
            foreach (Maand m in klimatogram.Maanden)
            {
                if (latitude >= 0)
                {
                    if (_winterNoorderBreedte.Contains(m.Naam)) neerslag += m.Neerslag;
                }
                else
                {
                    if (_winterZuiderBreedte.Contains(m.Naam)) neerslag += m.Neerslag;
                }
            }
            return neerslag;
        }
    }

    public class ParameterAantalDrogeMaanden : Parameter
    {
        private readonly double _factor = 2.0;

        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.Count(m => m.Neerslag / _factor <= m.Temperatuur);
        }
        public override string GeefBeschrijving()
        {
            return "D";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            return Enumerable.Range(0, 13).Select(n => n.ToString()).ToList();
        }
    }

    public class ParameterGemiddeldeTemperatuurJaar : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.Average(m => m.Temperatuur);
        }
        public override string GeefBeschrijving()
        {
            return "Tj";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            throw new NotSupportedException(); //wordt niet gebruikt bij de vragen
        }
    }

    public class ParameterTotaleNeerslagJaar : Parameter
    {
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.Sum(m => m.Neerslag);
        }
        public override string GeefBeschrijving()
        {
            return "Nj";
        }
        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            throw new NotSupportedException(); //wordt niet gebruikt bij de vragen
        }
    }

   
    public class TemperatuurVierdeWarmsteMaandParameter : Parameter
    {

        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return klimatogram.Maanden.ToList().OrderByDescending(m => m.Temperatuur).ElementAt(3).Temperatuur;
        }

        public override string GeefBeschrijving()
        {
            return "De temperatuur van de vierde warmste maand";
        }

        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            throw new NotSupportedException();
        }
    }
}
