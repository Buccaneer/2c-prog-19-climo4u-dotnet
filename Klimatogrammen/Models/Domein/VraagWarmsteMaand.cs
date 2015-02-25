using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Models.Domein
{
    public class VraagWarmsteMaand : Vraag
    {
        public VraagWarmsteMaand(Klimatogram k) : base(k)
        {
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();

            Maand.Maanden.ForEach(m=>mogelijkeAntwoorden.Add(m.GeefNaam()));
            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Wat is de warmste maand?";
        }

        public override void ValideerVraag(string antwoord)
        {
            double hoogsteTemperatuur = _klimatogram.GemiddeldeTemperatuur.Max(t => t.Waarde);
            string correct = Maand.GeefMaand(new List<Temperatuur>(_klimatogram.GemiddeldeTemperatuur).IndexOf(hoogsteTemperatuur)).GeefNaam();
            
            if (correct.Equals(antwoord))
            {
                Resultaat = Resultaat.Juist;
                _antwoord = correct;
            }
            else
            {
                Resultaat = Resultaat.Fout;
                _antwoord = antwoord;
            }
           
        }
    }
}