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
        private ICollection<Maand> _maanden; 
        public virtual int BeginJaar { get; set; }
        public virtual int EindJaar { get; set; }
        public virtual Land Land { get; internal set; }
        public virtual double? Longitute { get; set; }
        public virtual double? Latitude { get; set; }

        public virtual ICollection<Maand> Maanden
        {
            get { return _maanden; } 
            set
            {
                value.ForEach(m=>m.Klimatogram=this);
                _maanden = value;
            }
        }

        public virtual string Locatie { get; set; }

        public Klimatogram(ICollection<Maand> maanden, double? latitude = null, double? longitute = null)
        {
            maanden.ForEach(m => m.Klimatogram = this);
            Maanden = maanden;
            Longitute = longitute;
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
            var g = Maanden.ToList();
            g.Sort();
            object klim = new
            {
                GemiddeldeTemperatuur = g.Select(maand => maand.Temperatuur).ToList(), //Maanden.Select(maand => maand.Temperatuur).ToList(),
                GemiddeldeNeerslag = g.Select(maand => maand.Neerslag).ToList(), //Maanden.Select(maand => maand.Neerslag).ToList(),
                BeginJaar,
                EindJaar,
                Land = Land.Naam,
                Locatie,
                TotaalGemiddeldeTemperatuur = GeefGemiddeldeTemperatuur(),
                TotaalNeerslag = GeefTotaleNeerslag(),
                Coordinaten = new {Longitute, Latitude}
            };
            return klim;
        }
    }
}
