using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Models.Domein
{
    public class VraagKoudsteMaand : Vraag
    {
        public VraagKoudsteMaand(Klimatogram k)
            : base(k)
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
            return "Wat is de koudste maand?";
        }

        public override void ValideerVraag(string antwoord)
        {
            double laagsteTemperatuur = _klimatogram.GemiddeldeTemperatuur.Min(t => t.Waarde);
            string correct = Maand.GeefMaand(new List<Temperatuur>(_klimatogram.GemiddeldeTemperatuur).IndexOf(laagsteTemperatuur)).GeefNaam();

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