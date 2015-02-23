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
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();

            foreach (Maand m in Enum.GetValues(typeof(Maand)))
            {
                mogelijkeAntwoorden.Add(m.ToString());
            }
            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Wat is de koudste maand?";
        }

        public override void ValideerVraag(string antwoord)
        {
            //klopt niet
            string correct = _klimatogram.GemiddeldeTemperatuur.Min().ToString();
            Resultaat = correct.Equals(antwoord) ? Resultaat.Juist : Resultaat.Fout;
        }
    }
}