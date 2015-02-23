using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.Domein
{
    public class VraagTemperatuurKoudsteMaand : Vraag
    {
        public VraagTemperatuurKoudsteMaand(Klimatogram k)
            : base(k)
        {
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();

            foreach (Temperatuur t in _klimatogram.GemiddeldeTemperatuur)
            {
                mogelijkeAntwoorden.Add(t.ToString());
            }
            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Wat is de temperatuur van de koudste maand (Tk)?";
        }

        public override void ValideerVraag(string antwoord)
        {
            string correct = _klimatogram.GemiddeldeTemperatuur.Min().ToString();
            Resultaat = correct.Equals(antwoord) ? Resultaat.Juist : Resultaat.Fout;
        }
    }
}