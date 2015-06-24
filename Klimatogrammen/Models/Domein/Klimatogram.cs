using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Models.Domein
{
    /// <summary>
    /// Klimatogram is een klasse die verscheidene zaken bijhoudt, zoals het beginjaar, eindjaar, het land en locatie, de longitude en latitude en een ICollection van maanden
    /// De neerslag en temperatuur kan via de maanden worden opgevraagd.
    /// Deze klasse bevat een methode om een klimatogramobject aan te maken, zodat dit geschikt is voor Json.
    /// </summary>
    public class Klimatogram
    {
        public virtual int BeginJaar { get; set; }
        public virtual int EindJaar { get; set; }
        public virtual Land Land { get; internal set; }
        public virtual double? Longitude { get; set; }
        public virtual double? Latitude { get; set; }
        public virtual List<Maand> Maanden { get; set; }
        public virtual string Locatie { get; set; }

        public Klimatogram(ICollection<Maand> maanden, double? latitude = null, double? longitude = null)
        {
            List<Maand> m = maanden.ToList();
            m.Sort();
            Maanden = m;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Klimatogram()
        {
        }

        public virtual ICollection<double> GeefTemperaturen()
        {
            return Maanden.Select(maand => maand.Temperatuur).ToList();
        }

        public virtual  ICollection<int> GeefNeerslagen()
        {
            return Maanden.Select(maand => maand.Neerslag).ToList();
        }

        public virtual int GeefTotaleNeerslag()
        {
            return Maanden.Sum(maand => maand.Neerslag);
        }

        public virtual double GeefGemiddeldeTemperatuur()
        {
            return Math.Round(Maanden.Average(maand => maand.Temperatuur),1);
        }

        public object MaakJsonObject()
        {
            object klim = new
            {
                GemiddeldeTemperatuur = Maanden.Select(maand => maand.Temperatuur).ToList(),
                GemiddeldeNeerslag = Maanden.Select(maand => maand.Neerslag).ToList(),
                BeginJaar,
                EindJaar,
                Land = Land.Naam,
                Locatie,
                TotaalGemiddeldeTemperatuur = GeefGemiddeldeTemperatuur(),
                TotaalNeerslag = GeefTotaleNeerslag(),
                Coordinaten = new {Longitute = Longitude, Latitude}
            };
            return klim;
        }
    }
}
