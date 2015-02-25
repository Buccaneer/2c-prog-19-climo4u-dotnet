using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Ninject.Infrastructure.Language;

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
                mogelijkeAntwoorden.Add(t.Waarde.ToString());
            }
            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Wat is de temperatuur van de koudste maand (Tk)?";
        }

        public override void ValideerVraag(string antwoord)
        {
            string correct = _klimatogram.GemiddeldeTemperatuur.Select(temp=>temp.Waarde).ToList().Min().ToString();
            _antwoord = antwoord;
            Resultaat = correct.Equals(antwoord) ? Resultaat.Juist : Resultaat.Fout;
        }
    }
}