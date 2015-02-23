using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.Domein
{
    public class VraagKoudsteMaand : Vraag
    {
        public VraagKoudsteMaand(Klimatogram k)
            : base(k)
        {
            double laagsteTemperatuur = _klimatogram.GemiddeldeTemperatuur.Min(t => t.Waarde);
            string correct = Maand.GeefMaand(new List<Temperatuur>(_klimatogram.GemiddeldeTemperatuur).IndexOf(laagsteTemperatuur)).GeefNaam();
            _antwoord = correct;
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();

       foreach (Maand maand in Maand.Maanden)
           mogelijkeAntwoorden.Add(maand.GeefNaam());

            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Wat is de koudste maand?";
        }

        public override void ValideerVraag(string antwoord)
        {

            Resultaat = _antwoord.Equals(antwoord) ? Resultaat.Juist : Resultaat.Fout;
        }
    }
}